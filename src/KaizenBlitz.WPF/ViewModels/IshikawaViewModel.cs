using CommunityToolkit.Mvvm.Input;
using KaizenBlitz.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using System.Windows;

namespace KaizenBlitz.WPF.ViewModels;

/// <summary>
/// ViewModel for Ishikawa Diagram
/// </summary>
public partial class IshikawaViewModel : ViewModelBase
{
    private readonly IToolRepository _toolRepository;
    private readonly IPdfService _pdfService;
    private Guid _projectId;
    private string _problemStatement = string.Empty;
    private bool _isCompleted;
    private IshikawaDiagram? _currentDiagram;

    public IshikawaViewModel(IToolRepository toolRepository, IPdfService pdfService)
    {
        _toolRepository = toolRepository;
        _pdfService = pdfService;
        Categories = new ObservableCollection<IshikawaCategoryViewModel>();
        InitializeDefaultCategories();
    }

    public ObservableCollection<IshikawaCategoryViewModel> Categories { get; }

    public string ProblemStatement
    {
        get => _problemStatement;
        set => SetProperty(ref _problemStatement, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }

    private void InitializeDefaultCategories()
    {
        var defaultCategories = new[] { "People", "Process", "Materials", "Equipment", "Environment", "Management" };
        foreach (var category in defaultCategories)
        {
            Categories.Add(new IshikawaCategoryViewModel { Name = category });
        }
    }

    public void LoadIshikawa(Guid projectId)
    {
        _projectId = projectId;
        LoadIshikawaAsync().ConfigureAwait(false);
    }

    private async Task LoadIshikawaAsync()
    {
        try
        {
            IsBusy = true;
            _currentDiagram = await _toolRepository.GetIshikawaByProjectIdAsync(_projectId);

            if (_currentDiagram != null)
            {
                ProblemStatement = _currentDiagram.ProblemStatement;
                IsCompleted = _currentDiagram.IsCompleted;

                Categories.Clear();
                foreach (var category in _currentDiagram.Categories)
                {
                    var causes = System.Text.Json.JsonSerializer.Deserialize<List<string>>(category.Causes) ?? new List<string>();
                    var viewModel = new IshikawaCategoryViewModel
                    {
                        Name = category.Name,
                        Causes = new ObservableCollection<string>(causes)
                    };
                    Categories.Add(viewModel);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading Ishikawa diagram: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;

            if (_currentDiagram == null)
            {
                _currentDiagram = new IshikawaDiagram
                {
                    ProjectId = _projectId
                };
            }

            _currentDiagram.ProblemStatement = ProblemStatement;
            _currentDiagram.IsCompleted = IsCompleted;

            _currentDiagram.Categories.Clear();
            foreach (var categoryVM in Categories)
            {
                var causesJson = System.Text.Json.JsonSerializer.Serialize(categoryVM.Causes.ToList());
                _currentDiagram.Categories.Add(new IshikawaCategory
                {
                    Name = categoryVM.Name,
                    Causes = causesJson
                });
            }

            await _toolRepository.SaveIshikawaAsync(_currentDiagram);

            MessageBox.Show("Ishikawa diagram saved successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error saving Ishikawa diagram: {ex.Message}";
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
        if (_currentDiagram == null)
        {
            MessageBox.Show("Please save the Ishikawa diagram first.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            IsBusy = true;

            var pdfBytes = await _pdfService.GenerateIshikawaPdfAsync(_currentDiagram);

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Ishikawa_{DateTime.Now:yyyyMMdd}.pdf"
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
        if (string.IsNullOrWhiteSpace(ProblemStatement))
        {
            MessageBox.Show("Please fill in the Problem Statement before marking as complete.",
                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        IsCompleted = true;
        SaveCommand.Execute(null);
    }
}

/// <summary>
/// Helper view model for Ishikawa categories
/// </summary>
public class IshikawaCategoryViewModel
{
    public string Name { get; set; } = string.Empty;
    public ObservableCollection<string> Causes { get; set; } = new();
}
