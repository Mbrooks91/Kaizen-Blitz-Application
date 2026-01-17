using CommunityToolkit.Mvvm.Input;
using KaizenBlitz.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;
using System.Windows;
using KaizenBlitz.Services;

namespace KaizenBlitz.WPF.ViewModels;

/// <summary>
/// ViewModel for Action Plan
/// </summary>
public partial class ActionPlanViewModel : ViewModelBase
{
    private readonly IToolRepository _toolRepository;
    private readonly IPdfService _pdfService;
    private readonly ExcelExportService _excelExportService;
    private Guid _projectId;
    private bool _isCompleted;
    private ActionPlan? _currentActionPlan;
    private TaskStatus? _filterByStatus;

    public ActionPlanViewModel(IToolRepository toolRepository, IPdfService pdfService, ExcelExportService excelExportService)
    {
        _toolRepository = toolRepository;
        _pdfService = pdfService;
        _excelExportService = excelExportService;
        Tasks = new ObservableCollection<ActionPlanTask>();
    }

    public ObservableCollection<ActionPlanTask> Tasks { get; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }

    public TaskStatus? FilterByStatus
    {
        get => _filterByStatus;
        set
        {
            if (SetProperty(ref _filterByStatus, value))
            {
                ApplyFilter();
            }
        }
    }

    public void LoadActionPlan(Guid projectId)
    {
        _projectId = projectId;
        LoadActionPlanAsync().ConfigureAwait(false);
    }

    private async Task LoadActionPlanAsync()
    {
        try
        {
            IsBusy = true;
            _currentActionPlan = await _toolRepository.GetActionPlanByProjectIdAsync(_projectId);

            Tasks.Clear();
            if (_currentActionPlan != null)
            {
                IsCompleted = _currentActionPlan.IsCompleted;
                foreach (var task in _currentActionPlan.Tasks)
                {
                    Tasks.Add(task);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading action plan: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void AddTask()
    {
        var newTask = new ActionPlanTask
        {
            TaskDescription = "New Task",
            Status = TaskStatus.NotStarted,
            Priority = "Medium",
            Deadline = DateTime.Now.AddDays(7)
        };

        Tasks.Add(newTask);
    }

    [RelayCommand]
    private void DeleteTask(ActionPlanTask task)
    {
        if (task != null)
        {
            var result = MessageBox.Show($"Delete task '{task.TaskDescription}'?", "Confirm Delete",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(task);
            }
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsBusy = true;

            if (_currentActionPlan == null)
            {
                _currentActionPlan = new ActionPlan
                {
                    ProjectId = _projectId
                };
            }

            _currentActionPlan.IsCompleted = IsCompleted;
            _currentActionPlan.Tasks.Clear();

            foreach (var task in Tasks)
            {
                _currentActionPlan.Tasks.Add(task);
            }

            await _toolRepository.SaveActionPlanAsync(_currentActionPlan);

            MessageBox.Show("Action plan saved successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error saving action plan: {ex.Message}";
            MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ExportExcelAsync()
    {
        if (_currentActionPlan == null || !Tasks.Any())
        {
            MessageBox.Show("Please add tasks to the action plan first.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            IsBusy = true;

            var excelBytes = await _excelExportService.ExportActionPlanToExcelAsync(_currentActionPlan);

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"ActionPlan_{DateTime.Now:yyyyMMdd}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                await File.WriteAllBytesAsync(saveFileDialog.FileName, excelBytes);
                MessageBox.Show("Excel exported successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error exporting Excel: {ex.Message}";
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
        if (!Tasks.Any())
        {
            MessageBox.Show("Please add at least one task before marking as complete.",
                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        IsCompleted = true;
        SaveCommand.Execute(null);
    }

    private void ApplyFilter()
    {
        // Reload with filter - implementation would need to maintain original list
        LoadActionPlanAsync().ConfigureAwait(false);
    }
}
