using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.ViewModels.Components.GroupProperty;

public class GridPropertyGroup
{
    public GroupProperties GetGridPropertyGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.GridProperty.ToString(),
            Visibility = VisibilityValue.Collapsed.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.Row.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Column.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Fusion.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Merged.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.ColumnSpan.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.RowSpan.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.SelectedElement.ToString(),
                    Type = PropertyTypes.String.ToString(),
                    Value = ESelectedElement.Cell.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.HideBorder.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Visible.ToString()
                }
            ]
        };
    }
}