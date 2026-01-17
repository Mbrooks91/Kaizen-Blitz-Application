using System.Windows.Controls;
using KaizenBlitz.WPF.ViewModels;

namespace KaizenBlitz.WPF.Views.Tools;

/// <summary>
/// Interaction logic for FiveWhysView.xaml
/// </summary>
public partial class FiveWhysView : Page
{
    public FiveWhysView(FiveWhysViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
