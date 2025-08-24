using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalText
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.Text.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.Text != null)
        {
            var value = component.Property?.Text;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Text.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.FontFamily != null)
        {
            var value = component.Property?.FontFamily;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FontFamily.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.FontWeight != null)
        {
            var value = component.Property?.FontWeight;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FontWeight.ToString(),
                Value = value == "bold" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.FontStyle != null)
        {
            var value = component.Property?.FontStyle;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FontStyle.ToString(),
                Value = value == "italic" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.FontSize != null)
        {
            var value = component.Property?.FontSize;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FontSize.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.TextAlign != null)
        {
            var value = component.Property?.TextAlign;
            var alignName = value == "left" ? "AlignLeft" :
                            value == "right" ? "AlignRight" :
                            value == "center" ? "AlignCenter" : "AlignJustify";
            group.Properties.Add(new Property
            {
                Name = alignName,
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.TextDecoration != null)
        {
            var value = component.Property?.TextDecoration;
            var alignName = value == "underline" ? "TextUnderline" :
                            value == "overline" ? "TextOverline" :
                            value == "through" ? "TextThrough" : "TextUnderline";
            group.Properties.Add(new Property
            {
                Name = alignName,
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Foreground != null)
        {
            var value = component.Property?.Foreground;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Color.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.TextWrapping != null)
        {
            var value = component.Property?.TextWrapping;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.TextWrap.ToString(),
                Value = value == "wrap" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.LineSpacing != null)
        {
            var value = component.Property?.LineSpacing;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.LineSpacing.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.TextTrimming != null)
        {
            var value = component.Property?.TextTrimming;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.TextTrimming.ToString(),
                Value = value == "ellipsis" ? "1" : "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        return group;
    }
}