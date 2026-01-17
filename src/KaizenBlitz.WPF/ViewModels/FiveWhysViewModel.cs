using CommunityToolkit.Mvvm.Input;
using KaizenBlitz.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using System.Windows;

namespace KaizenBlitz.WPF.ViewModels;

/// <summary>
/// ViewModel for Five Whys analysis
/// </summary>
public partial class FiveWhysViewModel : ViewModelBase
{
    private readonly IToolRepository _toolRepository;
    private readonly IPdfService _pdfService;
    private Guid _projectId;
    private string _problemStatement = string.Empty;
    private string _why1 = string.Empty;
    private string _why2 = string.Empty;
    private string _why3 = string.Empty;
    private string _why4 = string.Empty;
    private string _why5 = string.Empty;
    private string _rootCause = string.Empty;
    private bool _isCompleted;
    private FiveWhys? _currentFiveWhys;

    public FiveWhysViewModel(IToolRepository toolRepository, IPdfService pdfService)
    {
        _toolRepository = toolRepository;
        _pdfService = pdfService;
        AdditionalWhys = new ObservableCollection<string>();
    }

    public ObservableCollection<string> AdditionalWhys { get; }

    public string ProblemStatement
    {
        get => _problemStatement;
        set => SetProperty(ref _problemStatement, value);
    }

    public string Why1
    {
        get => _why1;
        set => SetProperty(ref _why1, value);
    }

    public string Why2
    {
        get => _why2;
        set => SetProperty(ref _why2, value);
    }

    public string Why3
    {
        get => _why3;
        set => SetProperty(ref _why3, value);
    }

    public string Why4
    {
        get => _why4;
        set => SetProperty(ref _why4, value);
    }

    public string Why5
    {
        get => _why5;
        set => SetProperty(ref _why5, value);
    }

    public string RootCause
    {
        get => _rootCause;
        set => SetProperty(ref _rootCause, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }

    public void LoadFiveWhys(Guid projectId)
    {
        _projectId = projectId;
        LoadFiveWhysAsync().ConfigureAwait(false);
    }

    private async Task LoadFiveWhysAsync()
    {
        try
        {
            IsBusy = true;
            _currentFiveWhys = await _toolRepository.GetFiveWhysByProjectIdAsync(_projectId);

            if (_currentFiveWhys != null)
            {
                ProblemStatement = _currentFiveWhys.ProblemStatement;
                Why1 = _currentFiveWhys.Why1;
                Why2 = _currentFiveWhys.Why2;
                Why3 = _currentFiveWhys.Why3;
                Why4 = _currentFiveWhys.Why4;
                Why5 = _currentFiveWhys.Why5;
                RootCause = _currentFiveWhys.RootCause;
                IsCompleted = _currentFiveWhys.IsCompleted;

                var additionalWhys = System.Text.Json.JsonSerializer.Deserialize<List<string>>(_currentFiveWhys.AdditionalWhys);
                AdditionalWhys.Clear();
                if (additionalWhys != null)
                {
                    foreach (var why in additionalWhys)
                    {
                        AdditionalWhys.Add(why);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading Five Whys: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void AddWhy()
    {
        AdditionalWhys.Add(string.Empty);
    }

    [RelayCommand]
    private void RemoveWhy(string why)
    {
        AdditionalWhys.Remove(why);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;

            var additionalWhysJson = System.Text.Json.JsonSerializer.Serialize(AdditionalWhys.ToList());

            if (_currentFiveWhys == null)
            {
                _currentFiveWhys = new FiveWhys
                {
                    ProjectId = _projectId
                };
            }

            _currentFiveWhys.ProblemStatement = ProblemStatement;
            _currentFiveWhys.Why1 = Why1;
            _currentFiveWhys.Why2 = Why2;
            _currentFiveWhys.Why3 = Why3;
            _currentFiveWhys.Why4 = Why4;
            _currentFiveWhys.Why5 = Why5;
            _currentFiveWhys.RootCause = RootCause;
            _currentFiveWhys.AdditionalWhys = additionalWhysJson;
            _currentFiveWhys.IsCompleted = IsCompleted;

            await _toolRepository.SaveFiveWhysAsync(_currentFiveWhys);

            MessageBox.Show("Five Whys analysis saved successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error saving Five Whys: {ex.Message}";
            MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ExportPdfAsync()
    {
        if (_currentFiveWhys == null)
        {
            MessageBox.Show("Please save the Five Whys analysis first.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            IsBusy = true;

            var pdfBytes = await _pdfService.GenerateFiveWhysPdfAsync(_currentFiveWhys);

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"FiveWhys_{DateTime.Now:yyyyMMdd}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                await File.WriteAllBytesAsync(saveFileDialog.FileName, pdfBytes);
                MessageBox.Show("PDF exported successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error exporting PDF: {ex.Message}";
            MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void MarkComplete()
    {
        if (string.IsNullOrWhiteSpace(ProblemStatement) || string.IsNullOrWhiteSpace(RootCause))
        {
            MessageBox.Show("Please fill in the Problem Statement and Root Cause before marking as complete.",
                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        IsCompleted = true;
        SaveCommand.Execute(null);
    }
}
