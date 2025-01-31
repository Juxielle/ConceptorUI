using System;
using System.Windows;
using ConceptorUi.ViewModels.Operations;
using ConceptorUI.ViewModels.ReusableComponent;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.ReusableComponent;

static class ComponentVisibility
{
    public static void SetVisibilities(this ComponentModel component)
    {
        foreach (var group in component.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                component.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.SetGroupOnlyVisibility(groupName, false);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.SetGroupOnlyVisibility(groupName, false);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.SetGroupVisibility(groupName, false);
        }
        
        foreach (var group in component.Body.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Visible.ToString())
                component.Body.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.SelfAlignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.Body.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() && group.Visibility != Visibility.Visible.ToString())
                component.Body.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
                component.Body.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                component.Body.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                component.Body.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Alignment.ToString())
                {
                    if (property.Name == PropertyNames.SpaceBetween.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.SpaceAround.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.SpaceEvery.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Height.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.He.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Ve.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Hve.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.FillColor.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Padding.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.FilePicker.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Visibility != Visibility.Visible.ToString())
                        component.Body.SetPropertyVisibility(groupName, propertyName);
                }
            }
        }
    }
}