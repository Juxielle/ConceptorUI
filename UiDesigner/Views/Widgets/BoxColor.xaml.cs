using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Senders;
using UiDesigner.Inputs;

namespace ConceptorUI.Views.Widgets;

public partial class BoxColor
{
    public BoxColor()
    {
        InitializeComponent();
        Menu.Command = new RelayCommand(OnSelect);
        Menu.Sender = Color;
    }

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(BoxColor),
            new PropertyMetadata(null));
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Brush), typeof(BoxColor),
            new FrameworkPropertyMetadata(new BrushConverter().ConvertFrom("#000000") as SolidColorBrush,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColorChanged));

    public Brush Color
    {
        get => (Brush)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    private void UpdateColor(Brush brush)
    {
        Menu.Sender = Color;
    }
    
    private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        (sender as BoxColor)!.UpdateColor((Brush)e.NewValue);
    }
    
    private void OnClick(object sender, MouseButtonEventArgs e)
    {
        if(!e.OriginalSource.Equals(BorderContent)) return;
        Menu.IsOpen = true;
    }
    
    private void OnSelect(object sender)
    {
        if(sender is not Brush brush) return;
        Color = brush;
        Command?.Execute(new PropertyActionSender
        {
            Name = Tag.ToString()!,
            Value = brush.ToString()
        });
    }
}