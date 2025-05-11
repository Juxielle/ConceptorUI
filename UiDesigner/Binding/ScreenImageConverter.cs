using System;
using System.Globalization;
using System.Windows.Data;
using ConceptorUI.Services;

namespace UiDesigner.Binding;

public class ScreenImageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var fileName = value != null ? value.ToString() : "default_mobile_traite.png";
        var image = ReadScreenImageService.GetImage(fileName!);
        
        return image;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return parameter?.ToString()!;
    }
}