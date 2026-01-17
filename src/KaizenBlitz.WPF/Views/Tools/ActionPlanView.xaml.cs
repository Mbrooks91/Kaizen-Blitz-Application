using System.Windows.Controls;
using KaizenBlitz.WPF.ViewModels;

namespace KaizenBlitz.WPF.Views.Tools;

/// <summary>
/// Interaction logic for ActionPlanView.xaml
/// </summary>
public partial class ActionPlanView : Page
{
    public ActionPlanView(ActionPlanViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
