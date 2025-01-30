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
                if (property.Name == PropertyNames.FilePicker.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SELECT FICHIER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FilePicker
                    });
                else if (property.Name == PropertyNames.Trash.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "DELETE COMPONENT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Trash
                    });
                else if (property.Name == PropertyNames.Copy.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "COPY COMPONENT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Copy
                    });
                else if (property.Name == PropertyNames.Paste.ToString())
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
                if (property.Name == PropertyNames.Width.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "WIDTH",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Width
                    });
                else if (property.Name == PropertyNames.Height.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "HEIGHT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Height
                    });
                else if (property.Name == PropertyNames.Gap.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "GAP",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Gap
                    });
            }
            else if (groupProperties.Name == GroupNames.Text.ToString())
            {
                if (property.Name == PropertyNames.FontFamily.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "FONT FAMILY",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FontFamily
                    });
                else if (property.Name == PropertyNames.FontWeight.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "FONT WEIGHT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FontWeight
                    });
                else if (property.Name == PropertyNames.FontStyle.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "FONT STYLE",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FontStyle
                    });
                else if (property.Name == PropertyNames.FontSize.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "FONT SIZE",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FontSize
                    });
                else if (property.Name == PropertyNames.Color.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "FOREGROUND",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Color
                    });
                else if (property.Name == PropertyNames.Text.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "TEXT",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Text
                    });
            }
            else if (groupProperties.Name == GroupNames.Appearance.ToString())
            {
                if (property.Name == PropertyNames.FillColor.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "BACKGROUND",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.FillColor
                    });
                else if (property.Name == PropertyNames.Margin.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "MARGIN",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Margin
                    });
                else if (property.Name == PropertyNames.Padding.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "PADDING",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Padding
                    });
                else if (property.Name == PropertyNames.Border.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "BORDER",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Border
                    });
                else if (property.Name == PropertyNames.BorderRadius.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "BORDER RADIUS",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.BorderRadius
                    });
                else if (property.Name == PropertyNames.Opacity.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "OPACITY",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.Opacity
                    });
            }
            else if (groupProperties.Name == GroupNames.Shadow.ToString())
            {
                if (property.Name == PropertyNames.ShadowDepth.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SHADOW DEPTH",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.ShadowDepth
                    });
                else if (property.Name == PropertyNames.BlurRadius.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "BLUR RADIUS",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.BlurRadius
                    });
                else if (property.Name == PropertyNames.ShadowDirection.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SHADOW DIRECTION",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.ShadowDirection
                    });
                else if (property.Name == PropertyNames.ShadowColor.ToString())
                    configs.Add(new PropertyConfig
                    {
                        Name = "SHADOW COLOR",
                        IsEditable = isEditable,
                        IsFocusable = true,
                        PropertyName = PropertyNames.ShadowColor
                    });
            }
        }

        return configs;
    }
}