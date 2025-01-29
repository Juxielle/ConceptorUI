using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UiDesigner.Classes;

namespace ConceptorUI.Views.Modals;

public partial class ComponentPropertyConfig
{
    private static ComponentPropertyConfig? _obj;

    public ComponentPropertyConfig()
    {
        InitializeComponent();
        _obj = this;
    }

    public static ComponentPropertyConfig Instance => _obj ?? new ComponentPropertyConfig();

    public void Refresh(object sender, string groupName)
    {
        LvProperty.Items.Clear();
        var properties = sender as ObservableCollection<PropertyConfig>;

        GroupTitle.Text = groupName;
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
        var checkBox = (CheckBox)sender;
        var tag = checkBox.Tag.ToString();
        
        Console.WriteLine(@$"Tag: {tag} -- Is checked: {checkBox.IsChecked}");
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
}