using ConceptorUI.Models;

namespace ConceptorUI.ViewModels.Components.GroupProperty;

public class AppearanceGroup
{
    public GroupProperties GetAppearanceGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.Appearance.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.FillColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.CMargin.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MarginBtnActif.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = PropertyNames.MarginLeft.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Margin.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MarginLeft.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MarginTop.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MarginRight.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MarginBottom.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.CPadding.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PaddingBtnActif.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = PropertyNames.PaddingLeft.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Padding.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PaddingLeft.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PaddingTop.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PaddingRight.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PaddingBottom.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.CBorder.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderBtnActif.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "BorderLeft",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderStyle.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderLeftWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderLeftColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderLeftStyle.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderTopWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderTopColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderTopStyle.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRightWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRightColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRightStyle.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderBottomWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderBottomColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderBottomStyle.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.CBorderRadius.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRadius.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Opacity.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                }
            ]
        };
    }
}