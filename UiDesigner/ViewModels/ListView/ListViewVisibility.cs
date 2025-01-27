using System;
using System.Windows;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.ListView;

static class ListViewVisibility
{
    public static void SetVisibilities(this ListViewModel listView)
    {
        foreach (var group in listView.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                listView.SetGroupOnlyVisibility(groupName);
                listView.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                listView.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                listView.SetGroupOnlyVisibility(groupName);
                listView.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                listView.SetGroupOnlyVisibility(groupName);
                listView.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                listView.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Visible.ToString())
                listView.SetGroupVisibility(groupName);

            foreach (var property in group.Properties)
            {
                if (group.Name == GroupNames.Alignment.ToString())
                {
                    if (property.Name == PropertyNames.Hl.ToString() && listView.IsVertical &&
                        property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Hr.ToString() && listView.IsVertical &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Hc.ToString() && listView.IsVertical &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Vt.ToString() && !listView.IsVertical &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Vb.ToString() && !listView.IsVertical &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.Vc.ToString() && !listView.IsVertical &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();

                    else if (property.Name == PropertyNames.SpaceBetween.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.SpaceAround.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.SpaceEvery.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();
                }
                else if (group.Name == GroupNames.Transform.ToString())
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
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Collapsed.ToString();

                    else if (property.Name == PropertyNames.Gap.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        property.Visibility = Visibility.Visible.ToString();
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

        foreach (var child in listView.Children)
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
                    if (group.Name == GroupNames.SelfAlignment.ToString())
                    {
                        if (property.Name == PropertyNames.Hl.ToString() && listView.IsVertical &&
                            property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Hr.ToString() && listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Hc.ToString() && listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Vt.ToString() && !listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Vb.ToString() && !listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Vc.ToString() && !listView.IsVertical &&
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

                        else if (property.Name == PropertyNames.He.ToString() && listView.IsVertical &&
                                 child.AllowExpanded() && property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.Ve.ToString() && !listView.IsVertical &&
                                 child.AllowExpanded(false) && property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                    }
                    else if (group.Name == GroupNames.Global.ToString())
                    {
                        if (property.Name == PropertyNames.MoveLeft.ToString() && !listView.IsVertical &&
                            property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.MoveRight.ToString() && !listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                        
                        else if (property.Name == PropertyNames.MoveTop.ToString() && listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();

                        else if (property.Name == PropertyNames.MoveBottom.ToString() && listView.IsVertical &&
                                 property.Visibility != Visibility.Visible.ToString())
                            property.Visibility = Visibility.Visible.ToString();
                    }
                }
            }
        }
    }
}