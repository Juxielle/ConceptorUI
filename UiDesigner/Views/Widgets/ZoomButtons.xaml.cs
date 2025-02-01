using System;
using System.Windows;
using System.Windows.Input;
using ConceptorUI.Enums;
using ConceptorUI.Senders;
using UiDesigner.Inputs;

namespace ConceptorUI.Views.Widgets;

public partial class ZoomButtons
{
    public ZoomButtons()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ZoomButtons),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();
        var senderAction = (SenderAction)Enum.Parse(typeof(SenderAction), tag!);
        
        Command?.Execute(new PropertySender
        {
            SenderAction = senderAction,
            Value = "1.2"
        });
    }
}