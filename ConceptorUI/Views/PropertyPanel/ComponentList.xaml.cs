using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Models;
using ConceptorUi.ViewModels;

namespace ConceptorUI.Views.PropertyPanel;

public partial class ComponentList
{
    private string? _selectedIndex = string.Empty;
    private int _clickCount;
    public ICommand? SendComponentCommand;

    public ComponentList()
    {
        InitializeComponent();
        _clickCount = 0;
    }

    public void Refresh(object sender)
    {
        if (sender == null!) return;
        var components = sender as List<ConceptorUi.ViewModels.Component>;

        Content.Children.Clear();
        for (var i = 0; i < components!.Count; i++)
        {
            var value = System.Text.Json.JsonSerializer.Serialize(components[i].OnSerializer());
            var serializer = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(value);
            var model = new ComponentModel();

            model.OnDeserializer(serializer!);
            model.CanSelectAll();
            
            var border = new Border
            {
                Tag = components[i].Id,
                Background = new BrushConverter().ConvertFrom("#f0f0f0") as SolidColorBrush,
                Padding = new Thickness(5),
                Margin = new Thickness(0, 0, 0, 10),
                CornerRadius = new CornerRadius(6),
                Child = model.ComponentView
            };
            border.MouseDown += OnComponentSelect;

            Content.Children.Add(border);
        }
    }

    private void OnComponentSelect(object sender, MouseButtonEventArgs e)
    {
        var id = (sender as Border)!.Tag.ToString();
        _clickCount++;
        
        if (id == _selectedIndex)
        {
            if (_clickCount != 2) return;
            SendComponentCommand?.Execute(id);
            _clickCount = 0;
            return;
        }

        for (var i = 0; i < Content.Children.Count; i++)
        {
            if (((FrameworkElement)Content.Children[i]).Tag.ToString() == id)
                ((Border)Content.Children[i]).Background =
                    new BrushConverter().ConvertFrom("#efe5fd") as SolidColorBrush;
            else
                ((Border)Content.Children[i]).Background =
                    new BrushConverter().ConvertFrom("#f0f0f0") as SolidColorBrush;
        }

        _selectedIndex = id;
    }
}