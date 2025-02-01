using System.Windows;
using System.Windows.Input;

namespace ConceptorUI.Views.PropertyPanel;

public partial class PanelActionButtons
{
    public PanelActionButtons()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PanelActionButtons),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}