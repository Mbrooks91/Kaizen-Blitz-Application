using CommunityToolkit.Mvvm.ComponentModel;

namespace KaizenBlitz.WPF.ViewModels.Base;

/// <summary>
/// Base class for all view models
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
    private bool _isBusy;
    private string _errorMessage = string.Empty;

    /// <summary>
    /// Indicates whether the view model is performing an operation
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    /// <summary>
    /// Error message to display to the user
    /// </summary>
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
}
