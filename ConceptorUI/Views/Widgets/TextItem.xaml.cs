using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TextDecorations = ConceptorUI.Enums.TextDecorations;

namespace ConceptorUI.Views.Widgets;

public partial class TextItem
{
    public ICommand? Command { get; set; }
    
    public TextItem()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextItem),
            new PropertyMetadata(string.Empty));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public new static readonly DependencyProperty FontFamilyProperty =
        DependencyProperty.Register(nameof(FontFamily), typeof(FontFamily), typeof(TextItem),
            new PropertyMetadata(new FontFamily("Arial")));

    public new FontFamily FontFamily
    {
        get => (FontFamily)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }
    
    public new static readonly DependencyProperty FontWeightProperty =
        DependencyProperty.Register(nameof(FontWeight), typeof(FontWeight), typeof(TextItem),
            new PropertyMetadata(FontWeights.Normal));

    public new FontWeight FontWeight
    {
        get => (FontWeight)GetValue(FontWeightProperty);
        set => SetValue(FontWeightProperty, value);
    }
    
    public new static readonly DependencyProperty FontStyleProperty =
        DependencyProperty.Register(nameof(FontStyle), typeof(FontStyle), typeof(TextItem),
            new PropertyMetadata(FontStyles.Normal));

    public new FontStyle FontStyle
    {
        get => (FontStyle)GetValue(FontStyleProperty);
        set => SetValue(FontStyleProperty, value);
    }
    
    public static readonly DependencyProperty TextDecorationProperty =
        DependencyProperty.Register(nameof(TextDecoration), typeof(string), typeof(TextItem),
            new PropertyMetadata(TextDecorations.None.ToString()));

    public string TextDecoration
    {
        get => (string)GetValue(TextDecorationProperty);
        set => SetValue(TextDecorationProperty, value);
    }
    
    public new static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(TextItem),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush));

    public new Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }
    
    public new static readonly DependencyProperty BorderBrushProperty =
        DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(TextItem),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush));

    public new Brush BorderBrush
    {
        get => (Brush)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }
    
    public new static readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(TextItem),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#ffffff") as SolidColorBrush));

    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();

        switch (tag)
        {
            case "Text":
                Command?.Execute(
                    new Dictionary<string, object>{{"action", "Click"}, {"tag", Tag}}
                );
                break;
            case "ButtonUp":
                Command?.Execute(
                    new Dictionary<string, object>{{"action", "MoveUp"}, {"tag", Tag}}
                );
                break;
            case "ButtonDown":
                Command?.Execute(
                    new Dictionary<string, object>{{"action", "MoveDown"}, {"tag", Tag}}
                );
                break;
            case "ButtonDelete":
                Command?.Execute(
                    new Dictionary<string, object>{{"action", "Delete"}, {"tag", Tag}}
                );
                break;
        }
    }
}