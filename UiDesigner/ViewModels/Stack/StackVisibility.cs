using System;
using System.Windows;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace ConceptorUI.ViewModels.Stack;

static class StackVisibility
{
    public static void SetVisibilities(this StackModel stack)
    {
        foreach (var group in stack.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                stack.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                stack.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                stack.SetGroupOnlyVisibility(groupName);
                stack.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                stack.SetGroupOnlyVisibility(groupName);
                stack.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                stack.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Visible.ToString())
                stack.SetGroupVisibility(groupName);

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
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Ve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Hve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Stretch.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Gap.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
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
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Copy.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Paste.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Trash.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.CanSelect.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();
                }
            }
        }

        foreach (var child in stack.Children)
        {
            foreach (var group in child.PropertyGroups!)
            {
                var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

                if (group.Name == GroupNames.SelfAlignment.ToString() &&
                    group.Visibility != Visibility.Visible.ToString())
                {
                    child.SetGroupOnlyVisibility(groupName);
                    child.SetPropertiesOnlyVisibility(groupName, false);
                }
                else if (group.Name == GroupNames.Transform.ToString() &&
                         group.Visibility != Visibility.Visible.ToString())
                {
                    child.SetGroupOnlyVisibility(groupName);
                    child.SetPropertiesOnlyVisibility(groupName, false);
                }
                else if (group.Name == GroupNames.Global.ToString() &&
                         group.Visibility != Visibility.Visible.ToString())
                    child.SetGroupOnlyVisibility(groupName);

                foreach (var property in group.Properties)
                {
                    var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                    if (group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (property.Name == PropertyNames.Hl.ToString() &&
                            property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                        
                        else if (property.Name == PropertyNames.Hr.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                        
                        else if (property.Name == PropertyNames.Hc.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                        
                        else if (property.Name == PropertyNames.Vt.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                        
                        else if (property.Name == PropertyNames.Vb.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                        
                        else if (property.Name == PropertyNames.Vc.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                    }
                    else if (group.Name == GroupNames.Transform.ToString())
                    {
                        if (property.Name == PropertyNames.Width.ToString() &&
                            property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Height.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.He.ToString() && child.AllowExpanded() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Ve.ToString() && child.AllowExpanded(false) &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Hve.ToString() && child.AllowExpanded() &&
                                 child.AllowExpanded(false) && property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                    }
                    else if (group.Name == GroupNames.Global.ToString())
                    {
                        if (property.Name == PropertyNames.MoveLeft.ToString() &&
                                 property.Visibility != Visibility.Collapsed.ToString())
                            property.Visibility = Visibility.Collapsed.ToString();
                        
                        else if (property.Name == PropertyNames.MoveRight.ToString() &&
                                 property.Visibility != Visibility.Collapsed.ToString())
                            property.Visibility = Visibility.Collapsed.ToString();
                    }
                }
            }
        }
    }
}