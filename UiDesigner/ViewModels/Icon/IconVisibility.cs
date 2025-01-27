using System;
using System.Windows;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.Icon;

static class IconVisibility
{
    public static void SetVisibilities(this IconModel icon)
    {
        foreach (var group in icon.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                icon.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                icon.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                icon.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
                icon.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                icon.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                icon.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Height.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.FillColor.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName);
                    if (property.Name == PropertyNames.Margin.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.FilePicker.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        icon.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }
    }
}