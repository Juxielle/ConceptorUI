using System;
using System.Collections.Generic;
using ConceptorUI.Models;
using ConceptorUi.ViewModels;

namespace ConceptorUI.Views.PropertyPanel;

public partial class ComponentList
{
    public ComponentList()
    {
        InitializeComponent();
    }

    public void Refresh(object sender)
    {
        if(sender == null!) return;
        var components = sender as List<ConceptorUi.ViewModels.Component>;
        
        Content.Children.Clear();
        foreach (var component in components!)
        {
            var value = System.Text.Json.JsonSerializer.Serialize(component.OnSerializer());
            var serializer = System.Text.Json.JsonSerializer.Deserialize<CompSerializer>(value);
            var model = new ComponentModel();
            
            model.OnDeserializer(serializer!);
            model.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#dddddd");
            
            Content.Children.Add(model.ComponentView);
        }
    }
}