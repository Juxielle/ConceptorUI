using ConceptorUI.Models;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents.PropertySettings;

public static class ExternalConverter
{
    public static void Convert(this ExternalComponent externalComponent, CompSerializer compSerializer)
    {
        var component = ComponentHelper.GetComponent(compSerializer.Name!);
        externalComponent.Name = component.Name.ToString();
        
        for (var i = 0; i < component.PropertyGroups!.Count; i++)
        {
            for (var j = 0; j < component.PropertyGroups[i].Properties.Count; j++)
            {
                if(component.PropertyGroups[i].Properties[j].Name != compSerializer.Properties![i].Properties[j].Name ||
                   component.PropertyGroups[i].Properties[j].Value == compSerializer.Properties![i].Properties[j].Value) continue;

                var name = compSerializer.Properties![i].Properties[j].Name;
                var value = compSerializer.Properties![i].Properties[j].Value;
                
                if (compSerializer.Properties![i].Name == GroupNames.Alignment.ToString())
                {
                    externalComponent.Property!.HorizontalAlignment = name == "Hl" ? "left" :
                                                                      name == "Hr" ? "right" :
                                                                      name == "Hc" ? "center" :
                                                                      name == "SpaceBetween" ? "between" :
                                                                      name == "SpaceAround" ? "around" :
                                                                      name == "SpaceEvery" ? "every" : "none";
                }
            }
        }
        
        if(compSerializer.Children == null || compSerializer.Children.Count == 0) return;

        foreach (var child in compSerializer.Children)
        {
            externalComponent.Convert(child);
        }
    }
}