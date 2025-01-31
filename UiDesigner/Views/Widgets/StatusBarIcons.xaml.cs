using System.Windows;
using System.Windows.Media;

namespace ConceptorUI.Views.Widgets;

public partial class StatusBarIcons
{
    public StatusBarIcons()
    {
        InitializeComponent();
    }

    public new static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(StatusBarIcons),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#BB86FC") as SolidColorBrush));

    public new Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }
}