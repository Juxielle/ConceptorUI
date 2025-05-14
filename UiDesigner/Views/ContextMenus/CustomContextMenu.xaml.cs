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

    public static readonly DependencyProperty SenderProperty =
        DependencyProperty.Register(nameof(Sender), typeof(object), typeof(CustomContextMenu),
            new PropertyMetadata(null));

    public object Sender
    {
        get => GetValue(SenderProperty);
        set => SetValue(SenderProperty, value);
    }
}