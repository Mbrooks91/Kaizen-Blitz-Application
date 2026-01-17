using System.Globalization;
using System.Windows.Data;

namespace KaizenBlitz.WPF.Helpers.Converters;

/// <summary>
/// Converts progress (0-100) to percentage string
/// </summary>
public class ProgressToPercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int progress)
        {
            return $"{progress}%";
        }
        if (value is double progressDouble)
        {
            return $"{progressDouble:F0}%";
        }
        return "0%";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            var cleaned = str.Replace("%", "").Trim();
            if (int.TryParse(cleaned, out var result))
            {
                return result;
            }
        }
        return 0;
    }
}
