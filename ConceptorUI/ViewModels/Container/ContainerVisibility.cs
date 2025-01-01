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
                container.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Appearance.ToString() && group.Visibility != Visibility.Visible.ToString())
                container.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
                container.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                container.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                container.SetGroupVisibility(groupName, false);
            
            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Alignment.ToString())
                {
                    if (property.Name == PropertyNames.SpaceBetween.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.SpaceAround.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.SpaceEvery.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Gap.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Stretch.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Rot.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Y.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Visibility != Visibility.Visible.ToString())
                        container.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.FilePicker.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        container.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Visibility != Visibility.Visible.ToString())
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