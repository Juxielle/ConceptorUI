using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ExternalComponents;

public static class ExternalAppearance
{
    public static GroupProperties Convert(this ExternalComponent component)
    {
        var group = new GroupProperties
        {
            Name = GroupNames.Appearance.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties = []
        };
        
        if (component.Property?.Background != null)
        {
            var value = component.Property?.Background;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.FillColor.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Opacity != null)
        {
            var value = component.Property?.Opacity;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Opacity.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Margin != null)
        {
            var value = component.Property?.Margin;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Margin.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.MarginLeft != null)
        {
            var value = component.Property?.MarginLeft;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginLeft.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.MarginRight != null)
        {
            var value = component.Property?.MarginRight;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginRight.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.MarginTop != null)
        {
            var value = component.Property?.MarginTop;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginTop.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.MarginBottom != null)
        {
            var value = component.Property?.MarginBottom;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginBottom.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.MarginHorizontal != null)
        {
            var value = component.Property?.MarginHorizontal;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginLeft.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginRight.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.MarginVertical != null)
        {
            var value = component.Property?.MarginVertical;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginTop.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.MarginBottom.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CMargin.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.Padding != null)
        {
            var value = component.Property?.Padding;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.Padding.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.PaddingLeft != null)
        {
            var value = component.Property?.PaddingLeft;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingLeft.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.PaddingRight != null)
        {
            var value = component.Property?.PaddingRight;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingRight.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.PaddingTop != null)
        {
            var value = component.Property?.PaddingTop;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingTop.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.PaddingBottom != null)
        {
            var value = component.Property?.PaddingBottom;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingBottom.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.PaddingHorizontal != null)
        {
            var value = component.Property?.PaddingHorizontal;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingLeft.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingRight.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.PaddingVertical != null)
        {
            var value = component.Property?.PaddingVertical;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingTop.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.PaddingBottom.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CPadding.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderColor != null)
        {
            var value = component.Property?.BorderColor;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderColor.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderWidth != null)
        {
            var value = component.Property?.BorderWidth;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderWidth.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorder.ToString(),
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderLeftWidth != null)
        {
            var value = component.Property?.BorderLeftWidth;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderLeftWidth.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorder.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderBtnActif.ToString(),
                Value = "BorderLeft",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderRightWidth != null)
        {
            var value = component.Property?.BorderRightWidth;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRightWidth.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorder.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderBtnActif.ToString(),
                Value = "BorderRight",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderTopWidth != null)
        {
            var value = component.Property?.BorderTopWidth;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderTopWidth.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorder.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderBtnActif.ToString(),
                Value = "BorderTop",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderBottomWidth != null)
        {
            var value = component.Property?.BorderBottomWidth;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderBottomWidth.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorder.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderBtnActif.ToString(),
                Value = "BorderTop",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderRadius != null)
        {
            var value = component.Property?.BorderRadius;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadius.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorderRadius.ToString(),
                Value = "1",
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderRadiusTopLeft != null)
        {
            var value = component.Property?.BorderRadiusTopLeft;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorderRadius.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadBtnActif.ToString(),
                Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderRadiusTopRight != null)
        {
            var value = component.Property?.BorderRadiusTopRight;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadiusTopRight.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorderRadius.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadBtnActif.ToString(),
                Value = PropertyNames.BorderRadiusTopRight.ToString(),
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderRadiusBottomLeft != null)
        {
            var value = component.Property?.BorderRadiusBottomLeft;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorderRadius.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadBtnActif.ToString(),
                Value = PropertyNames.BorderRadiusBottomLeft.ToString(),
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        if (component.Property?.BorderRadiusBottomRight != null)
        {
            var value = component.Property?.BorderRadiusBottomRight;
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                Value = value!,
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.CBorderRadius.ToString(),
                Value = "0",
                Visibility = VisibilityValue.Visible.ToString()
            });
            group.Properties.Add(new Property
            {
                Name = PropertyNames.BorderRadBtnActif.ToString(),
                Value = PropertyNames.BorderRadiusBottomRight.ToString(),
                Visibility = VisibilityValue.Visible.ToString()
            });
        }
        
        return group;
    }
}