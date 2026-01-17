using System.Windows.Controls;
using KaizenBlitz.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace KaizenBlitz.WPF.Views.Dashboard;

/// <summary>
/// Interaction logic for ProjectDashboardView.xaml
/// </summary>
public partial class ProjectDashboardView : Page
{
    public ProjectDashboardView()
    {
        InitializeComponent();
        
        // Get ViewModel from DI container
        if (App.Current is App app && app.GetType().GetProperty("ServiceProvider") != null)
        {
            var serviceProvider = (IServiceProvider?)app.GetType().GetProperty("ServiceProvider")?.GetValue(app);
            if (serviceProvider != null)
            {
                DataContext = serviceProvider.GetService<ProjectDashboardViewModel>();
            }
        }
    }
}
