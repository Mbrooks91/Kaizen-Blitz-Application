using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.WPF.Helpers.Converters;

/// <summary>
/// Converts ProjectStatus enum to color brush
/// </summary>
public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ProjectStatus status)
        {
            return status switch
            {
                ProjectStatus.InProgress => new SolidColorBrush(Color.FromRgb(0, 120, 212)), // Blue
                ProjectStatus.Completed => new SolidColorBrush(Color.FromRgb(16, 124, 16)), // Green
                ProjectStatus.OnHold => new SolidColorBrush(Color.FromRgb(255, 140, 0)), // Orange
                ProjectStatus.Cancelled => new SolidColorBrush(Color.FromRgb(232, 17, 35)), // Red
                _ => new SolidColorBrush(Colors.Gray)
            };
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
