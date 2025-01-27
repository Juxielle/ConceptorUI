using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UiDesigner.Binding;

public class BooleanBorderThicknessConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isTrue = value != null && (bool)value;
        var thickness = parameter != null ? System.Convert.ToInt32(parameter) : 0;
        
        return new Thickness(isTrue ? thickness : 0);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return parameter?.ToString()!;
    }
}