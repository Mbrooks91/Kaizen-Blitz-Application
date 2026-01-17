using CommunityToolkit.Mvvm.Input;
using KaizenBlitz.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;
using System.Windows;

namespace KaizenBlitz.WPF.ViewModels;

/// <summary>
/// ViewModel for the project dashboard
/// </summary>
public partial class ProjectDashboardViewModel : ViewModelBase
{
    private readonly IProjectRepository _projectRepository;
    private string _searchText = string.Empty;
    private ProjectStatus? _selectedStatusFilter;

    public ProjectDashboardViewModel(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
        Projects = new ObservableCollection<Project>();
        LoadProjectsAsync().ConfigureAwait(false);
    }

    public ObservableCollection<Project> Projects { get; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                SearchCommand.Execute(null);
            }
        }
    }

    public ProjectStatus? SelectedStatusFilter
    {
        get => _selectedStatusFilter;
        set
        {
            if (SetProperty(ref _selectedStatusFilter, value))
            {
                FilterProjectsAsync().ConfigureAwait(false);
            }
        }
    }

    [RelayCommand]
    private async Task LoadProjectsAsync()
    {
        try
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            var projects = await _projectRepository.GetAllAsync();
            
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading projects: {ex.Message}";
            MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadProjectsAsync();
            return;
        }

        try
        {
            IsBusy = true;
            var projects = await _projectRepository.SearchAsync(SearchText);
            
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error searching projects: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task FilterProjectsAsync()
    {
        try
        {
            IsBusy = true;
            IEnumerable<Project> projects;

            if (SelectedStatusFilter.HasValue)
            {
                var allProjects = await _projectRepository.GetAllAsync();
                projects = allProjects.Where(p => p.Status == SelectedStatusFilter.Value);
            }
            else
            {
                projects = await _projectRepository.GetAllAsync();
            }

            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error filtering projects: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void NewProject()
    {
        // Navigate to wizard - implementation depends on navigation service
        MessageBox.Show("New Project wizard will open here", "New Project");
    }

    [RelayCommand]
    private void OpenProject(Project project)
    {
        if (project != null)
        {
            MessageBox.Show($"Opening project: {project.Name}", "Open Project");
        }
    }

    [RelayCommand]
    private async Task DeleteProject(Project project)
    {
        if (project == null) return;

        var result = MessageBox.Show(
            $"Are you sure you want to delete project '{project.Name}'?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                await _projectRepository.DeleteAsync(project.ProjectId);
                Projects.Remove(project);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting project: {ex.Message}";
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
