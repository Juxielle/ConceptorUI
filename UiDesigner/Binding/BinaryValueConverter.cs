using System;
using System.Globalization;
using System.Windows.Data;

namespace UiDesigner.Binding;

public class BinaryValueConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != null && (string)value == "1" ? "#6739b7" : "#8c8c8a";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return parameter?.ToString()!;
    }
}