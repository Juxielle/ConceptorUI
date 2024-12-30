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
                row.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                row.SetGroupVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() && group.Visibility != Visibility.Visible.ToString())
                row.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Global.ToString() && group.Visibility != Visibility.Visible.ToString())
                row.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() && group.Visibility != Visibility.Collapsed.ToString())
                row.SetGroupVisibility(groupName, false);
            
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
                    else if (property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Stretch.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.FilePicker.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveLeft.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, !row.IsVertical);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName, row.IsVertical);
                    else if (property.Visibility != Visibility.Visible.ToString())
                        row.SetPropertyVisibility(groupName, propertyName);
                }
            }
        }
        
        foreach (var child in row.Children)
        {
            foreach (var group in child.PropertyGroups!)
            {
                var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

                if (group.Name == GroupNames.SelfAlignment.ToString() &&
                    group.Visibility != Visibility.Visible.ToString())
                    child.SetGroupOnlyVisibility(groupName);
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
                        else if (property.Visibility != Visibility.Collapsed.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, false);
                    }
                    else if (group.Name == GroupNames.Global.ToString())
                    {
                        if (property.Name == PropertyNames.FilePicker.ToString() &&
                            property.Visibility != Visibility.Collapsed.ToString())
                            child.SetPropertyVisibility(groupName, propertyName, false);
                        else if (property.Name == PropertyNames.MoveLeft.ToString() &&
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
                        else if (property.Visibility != Visibility.Visible.ToString())
                            child.SetPropertyVisibility(groupName, propertyName);
                    }
                }
            }
        }
    }
}