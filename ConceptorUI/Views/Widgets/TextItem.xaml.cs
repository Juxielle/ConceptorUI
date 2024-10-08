﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();

        switch (tag)
        {
            case "Text":
                Command?.Execute(
                    new Dictionary<string, object>{{"action", "Text"}, {"tag", Tag}}
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