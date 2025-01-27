using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace UiDesigner.Views.Widgets;

public partial class AppBox
{
    public AppBox()
    {
        InitializeComponent();

        ContentFilter.Visibility = Visibility.Collapsed;
        ContentTitle.Visibility = Visibility.Collapsed;
    }

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(AppBox),
            new PropertyMetadata(string.Empty));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(AppBox),
            new PropertyMetadata(null));

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    private void OnMouseEnter(object? sender, MouseEventArgs e)
    {
        ContentFilter.Visibility = Visibility.Visible;
        ContentTitle.Visibility = Visibility.Visible;
    }

    private void OnMouseLeave(object? sender, MouseEventArgs e)
    {
        ContentFilter.Visibility = Visibility.Collapsed;
        ContentTitle.Visibility = Visibility.Collapsed;
    }

    private void OnMouseDown(object? sender, MouseButtonEventArgs e)
    {
    }
}