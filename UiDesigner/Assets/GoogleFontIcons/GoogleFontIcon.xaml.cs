using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UiDesigner.Assets.GoogleFontIcons;

public partial class GoogleFontIcon
{
    public GoogleFontIcon()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush),
            typeof(GoogleFontIcon), new PropertyMetadata(Brushes.Black));

    public Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    public static readonly DependencyProperty FontSizeProperty =
        DependencyProperty.Register(nameof(FontSize), typeof(double),
            typeof(GoogleFontIcon), new PropertyMetadata(10d));

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly DependencyProperty IconNameProperty =
        DependencyProperty.Register(nameof(IconName), typeof(string),
            typeof(GoogleFontIcon), 
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIconNamePropertyChanged));

    public string IconName
    {
        get => (string)GetValue(IconNameProperty);
        set => SetValue(IconNameProperty, value);
    }

    private void UpdateIconName(string name)
    {
        var icon = GoogleFontIcons.GetIcons().Find(i => i.Name == name);
        if(icon == null) return;
        
        icon!.Icon.Foreground = Brushes.Black;
        icon.Icon.FontSize = 28;
        Child = icon.Icon;
    }

    private static void OnIconNamePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        (sender as GoogleFontIcon)!.UpdateIconName((string)e.NewValue);
    }

    public object GetIcon()
    {
        return Child;
    }
}