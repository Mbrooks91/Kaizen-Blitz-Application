using System.Windows;
using KaizenBlitz.WPF.ViewModels;

namespace KaizenBlitz.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
