using CommunityToolkit.Mvvm.Input;
using KaizenBlitz.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using System.Windows;

namespace KaizenBlitz.WPF.ViewModels;

/// <summary>
/// ViewModel for the main window
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IProjectRepository _projectRepository;
    private string _currentView = "Dashboard";

    public MainWindowViewModel(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public string CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    [RelayCommand]
    private void NavigateToDashboard()
    {
        CurrentView = "Dashboard";
    }

    [RelayCommand]
    private void Exit()
    {
        Application.Current.Shutdown();
    }
}
