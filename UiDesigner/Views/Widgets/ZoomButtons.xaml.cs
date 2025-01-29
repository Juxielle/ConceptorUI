using System.Windows;
using System.Windows.Input;

namespace ConceptorUI.Views.Widgets;

public partial class ZoomButtons
{
    public ZoomButtons()
    {
        InitializeComponent();
    }
    
    public ICommand ZoomButtonCommand { get; }

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ZoomButtons),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}