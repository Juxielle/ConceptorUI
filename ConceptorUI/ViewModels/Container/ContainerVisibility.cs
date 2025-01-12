using System;
using System.Windows;
using ConceptorUI.Models;
using ConceptorUI.ViewModels.Container;

namespace ConceptorUi.ViewModels.Container;

static class ContainerVisibility
{
    //Ajout des groupes et des propriétés manquants
    public static void SetVisibilities(this ContainerModel container)
    {
        foreach (var group in container.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);
            
            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                container.SetGroupOnlyVisibility(groupName);
                container.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Appearance.ToString() && group.Visibility != Visibility.Visible.ToString())
                container.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                container.SetGroupOnlyVisibility(groupName);
                container.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                container.SetGroupOnlyVisibility(groupName);
                container.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                container.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Visible.ToString())
                container.SetGroupVisibility(groupName);
            
            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);
                
                if (group.Name == GroupNames.Alignment.ToString())
                {
                    if (property.Name == PropertyNames.Hl.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Hr.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Hc.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Vt.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Vb.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Vc.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Height.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.He.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Ve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Hve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Gap.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);//Très important
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.Copy.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Paste.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Trash.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                }
            }
        }
        
        
        if (container.Children.Count == 0) return;
        var component = container.Children[0];

        foreach (var group in component.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.SelfAlignment.ToString() &&
                group.Visibility != Visibility.Collapsed.ToString())
            {
                component.SetGroupVisibility(groupName, false);
                break;
            }
        }
    }
}