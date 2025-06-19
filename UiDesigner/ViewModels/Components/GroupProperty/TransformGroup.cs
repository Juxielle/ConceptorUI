using ConceptorUI.Enums;
using ConceptorUI.Models;
using UiDesigner.Enums;
using UiDesigner.Models;

namespace ConceptorUI.ViewModels.Components.GroupProperty;

public class TransformGroup
{
    public GroupProperties GetTransformGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.Transform.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.Width.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = SizeValue.Expand.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Height.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = SizeValue.Expand.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PreviewWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = SizeValue.Expand.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.PreviewHeight.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = SizeValue.Expand.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.X.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Y.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Rot.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.He.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Ve.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Hve.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Stretch.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = ImageStretch.Fill.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Gap.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Shape.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = Shapes.Rectangle.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString()
                },
                
                new Property
                {
                    Name = PropertyNames.ApparentWidth.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.ApparentHeight.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },
            ]
        };
    }
}