using System.Collections.ObjectModel;
using UiDesigner.Classes;
using UiDesigner.Models;

namespace ConceptorUI.Services;

public static class PropertiesConfigService
{
    public static ObservableCollection<PropertyConfig> GetConfigs(GroupProperties groupProperties)
    {
        var configs = new ObservableCollection<PropertyConfig>();

        foreach (var property in groupProperties.Properties)
        {
            if (property.Visibility != VisibilityValue.Visible.ToString()) continue;

            var isEditable = property.ComponentVisibility == VisibilityValue.Visible.ToString();
            if (groupProperties.Name == GroupNames.Global.ToString())
            {
                if (property.Name != PropertyNames.FilePicker.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SELECT FICHIER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FilePicker
                    });
                else if (property.Name != PropertyNames.Trash.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "DELETE COMPONENT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Trash
                    });
                else if (property.Name != PropertyNames.Copy.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "COPY COMPONENT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Copy
                    });
                else if (property.Name != PropertyNames.Paste.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "PASTE COMPONENT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Paste
                    });
            }
            else if (groupProperties.Name == GroupNames.Transform.ToString())
            {
                if (property.Name != PropertyNames.FilePicker.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SELECT FICHIER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FilePicker
                    });
            }
            else if (groupProperties.Name == GroupNames.Text.ToString())
            {
                if (property.Name != PropertyNames.FilePicker.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SELECT FICHIER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FilePicker
                    });
            }
            else if (groupProperties.Name == GroupNames.Appearance.ToString())
            {
                if (property.Name != PropertyNames.FilePicker.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SELECT FICHIER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FilePicker
                    });
            }
            else if (groupProperties.Name == GroupNames.Shadow.ToString())
            {
                if (property.Name != PropertyNames.FilePicker.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SELECT FICHIER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FilePicker
                    });
            }
        }

        return configs;
    }
}