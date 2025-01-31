using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Enums;
using ConceptorUI.Senders;
using ConceptorUI.Services;
using ConceptorUI.Views.Component;
using UiDesigner.Classes;
using UiDesigner.Models;

namespace ConceptorUI.Views.Modals;

public partial class ComponentPropertyConfig
{
    private static ComponentPropertyConfig? _obj;
    private readonly TransferService _transferService;
    private GroupNames _groupName;

    public ComponentPropertyConfig()
    {
        InitializeComponent();
        _obj = this;
        _groupName = GroupNames.Global;
        _transferService = new TransferService();
    }

    public static ComponentPropertyConfig Instance => _obj ?? new ComponentPropertyConfig();

    public void Refresh(object sender, string title, GroupNames groupName)
    {
        LvProperty.ItemsSource = null;
        LvProperty.Items.Clear();
        var properties = sender as ObservableCollection<PropertyConfig>;

        GroupTitle.Text = title;
        _groupName = groupName;
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

        var visibility = checkBox.IsChecked == true
            ? VisibilityValue.Visible.ToString()
            : VisibilityValue.Collapsed.ToString();
        
        var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), tag!);
        _transferService.Transfer<object>(nameof(PageView), new PropertySender
        {
            SenderAction = SenderAction.UpdatePropertyVisibility,
            GroupName = _groupName,
            propertyName = propertyName,
            Value = visibility
        });
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
}