using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ConceptorUI.Views.ContextMenus;

public partial class CustomContextMenu
{
    public CustomContextMenu()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CustomContextMenu),
            new PropertyMetadata(null));
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Brush), typeof(CustomContextMenu),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000") as SolidColorBrush));

    public Brush Color
    {
        get => (Brush)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
}