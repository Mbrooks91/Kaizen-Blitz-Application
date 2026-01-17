using System.Windows;
using KaizenBlitz.WPF.ViewModels;

namespace KaizenBlitz.WPF.Views.Wizard;

/// <summary>
/// Interaction logic for ProjectWizardView.xaml
/// </summary>
public partial class ProjectWizardView : Window
{
    public ProjectWizardView(ProjectWizardViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
