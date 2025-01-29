using ConceptorUI.Models;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.Components.GroupProperty;

public class SelfAlignmentGroup
{
    public GroupProperties GetSelfAlignmentGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.SelfAlignment.ToString(),
            Visibility = VisibilityValue.Collapsed.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.Hl.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Hc.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Hr.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Vt.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Vc.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Vb.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                }
            ]
        };
    }
}