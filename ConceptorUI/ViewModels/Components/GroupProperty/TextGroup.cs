using ConceptorUI.Models;

namespace ConceptorUI.ViewModels.Components.GroupProperty;

public class TextGroup
{
    public GroupProperties GetTextGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.Text.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.FontFamily.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "Arial",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.FontWeight.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.FontStyle.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.FontSize.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "12",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.AlignLeft.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.AlignCenter.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.AlignRight.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.AlignJustify.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.ListOrd.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.ListNOrd.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TabLeft.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TabRight.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextUnderline.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextOverline.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextThrough.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextError.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextIndex.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextExpo.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Color.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "#000000",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Text.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "My Text",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.DisplayText.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextWrap.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.LineSpacing.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.CurrentTextIndex.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.TextTrimming.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                }
            ]
        };
    }
}