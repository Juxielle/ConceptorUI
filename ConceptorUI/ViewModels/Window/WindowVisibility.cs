using System;
using System.Windows;
using ConceptorUI.Models;

namespace ConceptorUI.ViewModels.Window;

static class WindowVisibility
{
    public static void SetVisibilities(this WindowModel window)
    {
        foreach (var group in window.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                window.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                window.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Height.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }

        /* Status */
        foreach (var group in window.Statusbar.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                window.Statusbar.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                window.Statusbar.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.FillColor.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Statusbar.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Statusbar.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Statusbar.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        window.Statusbar.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Statusbar.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }

        /* Body */
        foreach (var group in window.Body.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Body.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Body.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                window.Body.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Body.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                window.Body.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Body.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.FillColor.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }

        /* Layout */
        foreach (var group in window.Layout.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Layout.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Layout.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                window.Layout.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Layout.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                window.Layout.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Layout.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.FillColor.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }
    }
}