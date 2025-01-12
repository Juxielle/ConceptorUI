using System;
using System.Windows;
using ConceptorUI.Models;

namespace ConceptorUI.ViewModels.Row;

static class RowVisibility
{
    public static void SetVisibilities(this RowModel row)
    {
        foreach (var group in row.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);
            
            if (group.Name == GroupNames.Alignment.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                row.SetGroupOnlyVisibility(groupName);
                row.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                row.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                row.SetGroupOnlyVisibility(groupName);
                row.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
            {
                row.SetGroupOnlyVisibility(groupName);
                row.SetPropertiesOnlyVisibility(groupName, false);
            }
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                row.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Shadow.ToString() && group.Visibility != Visibility.Visible.ToString())
                row.SetGroupVisibility(groupName);
            
            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);
                
                if (group.Name == GroupNames.Alignment.ToString())
                {
                    if (property.Name == PropertyNames.Hl.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                    else if (property.Name == PropertyNames.Hr.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                    else if (property.Name == PropertyNames.Hc.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                    else if (property.Name == PropertyNames.Vt.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                    else if (property.Name == PropertyNames.Vb.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                    else if (property.Name == PropertyNames.Vc.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                    else if (property.Name == PropertyNames.SpaceBetween.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.SpaceAround.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.SpaceEvery.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Height.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.He.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Ve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Hve.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.Copy.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Paste.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Trash.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveChildToParent.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.MoveParentToChild.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                }
            }
        }
        
        /* Sur les enfants, il faut simplement agir sur les props qu'on connaît. */
        foreach (var child in row.Children)
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
                            child.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                        else if (property.Name == PropertyNames.Hr.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                        else if (property.Name == PropertyNames.Hc.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                        else if (property.Name == PropertyNames.Vt.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                        else if (property.Name == PropertyNames.Vb.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                        else if (property.Name == PropertyNames.Vc.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                    }
                    else if (group.Name == GroupNames.Transform.ToString())
                    {
                        if (property.Name == PropertyNames.Width.ToString() &&
                            property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName);
                        else if (property.Name == PropertyNames.Height.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName);
                        else if (property.Name == PropertyNames.He.ToString() && child.AllowExpanded() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName);
                        else if (property.Name == PropertyNames.Ve.ToString() && child.AllowExpanded(false) &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName);
                        else if (property.Name == PropertyNames.Hve.ToString() && child.AllowExpanded() &&
                                 child.AllowExpanded(false) && property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName);
                    }
                    else if (group.Name == GroupNames.Global.ToString())
                    {
                        if (property.Name == PropertyNames.MoveLeft.ToString() &&
                                 property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                        else if (property.Name == PropertyNames.MoveRight.ToString() &&
                                property.Visibility != Visibility.Visible.ToString())
                           child.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                        else if (property.Name == PropertyNames.MoveTop.ToString() &&
                                property.Visibility != Visibility.Visible.ToString())
                           child.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                        else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                                property.Visibility != Visibility.Visible.ToString())
                           child.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                    }
                }
            }
        }
    }
}