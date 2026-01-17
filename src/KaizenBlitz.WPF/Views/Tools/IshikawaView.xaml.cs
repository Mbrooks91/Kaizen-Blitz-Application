using System.Windows.Controls;
using KaizenBlitz.WPF.ViewModels;

namespace KaizenBlitz.WPF.Views.Tools;

/// <summary>
/// Interaction logic for IshikawaView.xaml
/// </summary>
public partial class IshikawaView : Page
{
    public IshikawaView(IshikawaViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
