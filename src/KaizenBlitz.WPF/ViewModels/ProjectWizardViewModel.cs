using CommunityToolkit.Mvvm.Input;
using KaizenBlitz.WPF.ViewModels.Base;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;
using System.Windows;

namespace KaizenBlitz.WPF.ViewModels;

/// <summary>
/// ViewModel for the project wizard
/// </summary>
public partial class ProjectWizardViewModel : ViewModelBase
{
    private readonly IProjectRepository _projectRepository;
    private int _currentStep = 1;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _targetArea = string.Empty;
    private DateTime _startDate = DateTime.Now;
    private DateTime? _expectedCompletionDate;
    private string _teamMembers = string.Empty;

    public ProjectWizardViewModel(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public int CurrentStep
    {
        get => _currentStep;
        set => SetProperty(ref _currentStep, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string TargetArea
    {
        get => _targetArea;
        set => SetProperty(ref _targetArea, value);
    }

    public DateTime StartDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }

    public DateTime? ExpectedCompletionDate
    {
        get => _expectedCompletionDate;
        set => SetProperty(ref _expectedCompletionDate, value);
    }

    public string TeamMembers
    {
        get => _teamMembers;
        set => SetProperty(ref _teamMembers, value);
    }

    [RelayCommand]
    private void NextStep()
    {
        if (CurrentStep < 5)
        {
            CurrentStep++;
        }
    }

    [RelayCommand]
    private void PreviousStep()
    {
        if (CurrentStep > 1)
        {
            CurrentStep--;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            MessageBox.Show("Project name is required.", "Validation Error", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            IsBusy = true;

            // Convert team members string to JSON array
            var teamMembersList = TeamMembers
                .Split(new[] { ',', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(m => m.Trim())
                .Where(m => !string.IsNullOrEmpty(m))
                .ToList();

            var teamMembersJson = System.Text.Json.JsonSerializer.Serialize(teamMembersList);

            var project = new Project
            {
                Name = Name,
                Description = Description,
                TargetArea = TargetArea,
                StartDate = StartDate,
                ExpectedCompletionDate = ExpectedCompletionDate,
                Status = ProjectStatus.InProgress,
                CurrentPhase = KaizenPhase.Preparation,
                TeamMembers = teamMembersJson,
                ProgressPercentage = 0
            };

            await _projectRepository.CreateAsync(project);

            MessageBox.Show("Project created successfully!", "Success", 
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Close wizard window
            Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)?.Close();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error creating project: {ex.Message}";
            MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var result = MessageBox.Show("Are you sure you want to cancel? Unsaved changes will be lost.",
            "Confirm Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)?.Close();
        }
    }
}
