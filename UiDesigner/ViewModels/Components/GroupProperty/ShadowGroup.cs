using UiDesigner.Models;

namespace UiDesigner.ViewModels.Components.GroupProperty;

public class ShadowGroup
{
    public GroupProperties GetShadowGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.Shadow.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.ShadowDepth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.BlurRadius.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.ShadowDirection.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.ShadowColor.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ColorValue.Transparent.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                }
            ]
        };
    }
}