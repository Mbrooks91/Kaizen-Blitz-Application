using System.Windows;

namespace KaizenBlitz.WPF.Helpers;

/// <summary>
/// Service for navigation between views
/// </summary>
public class NavigationService
{
    private Window? _mainWindow;

    public void SetMainWindow(Window window)
    {
        _mainWindow = window;
    }

    public void NavigateTo<T>() where T : Window, new()
    {
        var window = new T();
        window.Show();
    }

    public void ShowDialog<T>() where T : Window, new()
    {
        var window = new T();
        window.ShowDialog();
    }
}
