using System;
using System.Collections.Generic;
using System.Text.Json;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Inputs;
using UiDesigner.Models;

namespace ConceptorUi.ViewModels.Operations;

static class SettingRc
{
    public static void OnUpdateComponent(this Component comp, CompSerializer sender)
    {
        if (comp.Children.Count == 0) return;

        var index = new List<int>();
        var found = false;

        foreach (var child in comp.Children)
        {
            if (sender.Id != child.Id)
            {
                child.OnUpdateComponent(sender);
                continue;
            }

            found = true;
            index.Add(comp.Children.IndexOf(child));
        }

        if (!found) return;

        foreach (var i in index)
        {
            var compSerialize = JsonSerializer.Serialize(comp.Children[i].OnSerializer());
            var senderSerialize = JsonSerializer.Serialize(sender);
            var senderDeSerialize = JsonSerializer.Deserialize<CompSerializer>(senderSerialize);

            var component = ComponentHelper.GetComponent(senderDeSerialize!.Name!);
            component.SelectedCommand = new RelayCommand(comp.OnSelectedHandle);
            component.OnDeserializer(senderDeSerialize);
            comp.Delete(i);
            comp.Children.Insert(i, component);
            comp.AddIntoChildContent(component.ComponentView, i);
            comp.ResetChildAlignment(i);
            comp.LayoutConstraints(i);

            var compDeSerialize = JsonSerializer.Deserialize<CompSerializer>(compSerialize);
            comp.OnUpdateProperties(compDeSerialize!, i);
        }
    }

    private static void OnUpdateProperties(this Component comp, CompSerializer sender, int i)
    {
        if (comp.Children[i].Id != sender.Id) return;

        var groups = sender.Properties;
        foreach (var group in groups!)
        {
            var groupEnum = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);
            foreach (var property in group.Properties)
            {
                if (property.ComponentVisibility != VisibilityValue.Visible.ToString()) continue;

                var propertyEnum = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);
                if (groupEnum == GroupNames.Global)
                {
                    if (propertyEnum == PropertyNames.FilePicker)
                        comp.Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                }
                else if (groupEnum == GroupNames.Text)
                {
                    comp.Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                }
                else if (groupEnum == GroupNames.Appearance)
                {
                    comp.Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                }
                else if (groupEnum == GroupNames.Shadow)
                {
                    comp.Children[i].OnUpdated(groupEnum, propertyEnum, property.Value, true);
                }
            }
        }

        for (var k = 0; k < comp.Children[k].Children.Count; k++)
        {
            if (k >= sender.Children!.Count ||
                sender.Children[k].GetType().Name == comp.Children[i].Children[k].GetType().Name) continue;
            comp.Children[i].Children[k].OnUpdateProperties(sender.Children![k], k);
        }
    }
    
    public static Dictionary<string, dynamic> GetCompatibility(this Component comp, CompSerializer sender)
    {
        var idCount = 0;
        var isExact = true;

        idCount = comp.Id == sender.Id ? idCount + 1 : idCount;
        var isCountExact = comp.Id == sender.Id;
        isExact = isExact && comp.Name.ToString() == sender.Name;
        isExact = isExact && sender.Children != null && comp.Children.Count == sender.Children.Count;
        var count = isExact && sender.Children != null ? comp.Children.Count : 0;
        
        for (var i = 0; i < count; i++)
        {
            var data = comp.Children[i].GetCompatibility(sender.Children![i]);
            idCount += data["idCount"];
            isExact = isExact && data["isExact"];
            isCountExact = isCountExact && data["isCountExact"];
        }
        
        return new Dictionary<string, dynamic>
        {
            { "idCount", idCount },
            { "isExact", isExact },
            { "isCountExact", isCountExact },
        };
    }
    
    public static void RestoreComponent(this Component comp, CompSerializer sender)
    {
        comp.Id = sender.Id;
        if(sender.Children == null || 
           comp.Children.Count == sender.Children.Count) return;
        for (var i = 0; i < comp.Children.Count; i++)
        {
            comp.Children[i].Id = sender.Children[i].Id;
            comp.RestoreComponent(sender.Children[i]);
        }
    }
}