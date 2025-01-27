using UiDesigner.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using UiDesigner.Classes;

namespace UiDesigner.Views.Modals;

public partial class ComponentPropertyConfig
{
    private static ComponentPropertyConfig? _obj;

    public ComponentPropertyConfig()
    {
        InitializeComponent();
        _obj = this;
    }

    public static ComponentPropertyConfig Instance => _obj ?? new ComponentPropertyConfig();

    public void Refresh(object sender)
    {
        LvProperty.Items.Clear();
        var properties = sender as ObservableCollection<PropertyConfig>;
        LvProperty.ItemsSource = properties;
        Show();
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();

        switch (tag)
        {
            case "Close":
                Hide();
                break;
        }
    }

    private void OnColorChecked(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(@"----------------------");
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
}