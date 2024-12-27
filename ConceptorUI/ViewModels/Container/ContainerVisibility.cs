using System;
using System.Windows;
using ConceptorUI.Models;

namespace ConceptorUi.ViewModels.Container;

static class ContainerVisibility
{
    //Ajout des groupes et des propriétés manquants
    public static void SetVisibilities(ContainerModel container)
    {
        foreach (var group in container.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);
            
            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Visible.ToString())
                container.SetGroupVisibility(groupName);
            
            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);
                
                if (group.Name == GroupNames.Alignment.ToString())
                {
                    if (property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                }
            }
        }
    }
}