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
    private int _selectedIndex = -1;

    public ComponentList()
    {
        InitializeComponent();
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
                Tag = i,
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
        var index = Convert.ToInt32((sender as Border)!.Tag.ToString());
        if (index == _selectedIndex) return;

        for (var i = 0; i < Content.Children.Count; i++)
        {
            if (i == index)
                ((Border)Content.Children[i]).Background =
                    new BrushConverter().ConvertFrom("#efe5fd") as SolidColorBrush;
            else
                ((Border)Content.Children[i]).Background =
                    new BrushConverter().ConvertFrom("#f0f0f0") as SolidColorBrush;
        }
    }
}