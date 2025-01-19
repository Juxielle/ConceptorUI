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
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
            {
                window.SetGroupOnlyVisibility(groupName);
                window.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                window.SetGroupOnlyVisibility(groupName);
                window.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Height.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.X.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Y.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Rot.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.He.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Ve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Hve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Stretch.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Gap.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.SelectedMode.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.FilePicker.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Focus.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Copy.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Paste.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Trash.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.CanSelect.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();
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
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
            {
                window.Statusbar.SetGroupOnlyVisibility(groupName);
                window.Statusbar.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                window.Statusbar.SetGroupOnlyVisibility(groupName);
                window.Statusbar.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                window.Statusbar.SetGroupVisibility(groupName, false);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.FillColor.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                    {
                        window.Statusbar.SetPropertyVisibility(groupName, propertyName);
                    }
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.SelectedMode.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.FilePicker.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Focus.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Copy.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Paste.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Trash.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.CanSelect.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();
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
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Collapsed.ToString())
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
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Body.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
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
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Collapsed.ToString())
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
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        window.Layout.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }
    }
}