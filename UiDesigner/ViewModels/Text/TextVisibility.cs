using System;
using System.Windows;
using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.Text;

static class TextVisibility
{
    public static void SetVisibilities(this TextModel text)
    {
        foreach (var group in text.PropertyGroups!)
        {
            var groupName = (GroupNames)Enum.Parse(typeof(GroupNames), group.Name);

            if (group.Name == GroupNames.Alignment.ToString() &&
                group.Visibility != Visibility.Collapsed.ToString())
                text.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.SelfAlignment.ToString() &&
                     group.Visibility != Visibility.Collapsed.ToString())
                text.SetGroupVisibility(groupName, false);
            else if (group.Name == GroupNames.Appearance.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                text.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Transform.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                text.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Global.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                text.SetGroupOnlyVisibility(groupName);
            else if (group.Name == GroupNames.Text.ToString() &&
                     group.Visibility != Visibility.Visible.ToString())
                text.SetGroupVisibility(groupName);

            foreach (var property in group.Properties)
            {
                var propertyName = (PropertyNames)Enum.Parse(typeof(PropertyNames), property.Name);

                if (group.Name == GroupNames.Transform.ToString())
                {
                    if (property.Name == PropertyNames.Width.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Height.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Stretch.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.He.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.Ve.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.Hve.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                }
                else if (group.Name == GroupNames.Text.ToString())
                {
                    if (property.Name == PropertyNames.Text.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.FontFamily.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.FontSize.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.FontWeight.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.FontStyle.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.AlignLeft.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.AlignCenter.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.AlignRight.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                    else if (property.Name == PropertyNames.AlignJustify.ToString() &&
                             property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    if (property.Name == PropertyNames.Margin.ToString() &&
                        property.Visibility != Visibility.Visible.ToString())
                        text.SetPropertyVisibility(groupName, propertyName);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    if (property.Name == PropertyNames.MoveLeft.ToString() &&
                        property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveRight.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveTop.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                    else if (property.Name == PropertyNames.MoveBottom.ToString() &&
                             property.Visibility != Visibility.Collapsed.ToString())
                        text.SetPropertyVisibility(groupName, propertyName, false);
                }
            }
        }
    }
}