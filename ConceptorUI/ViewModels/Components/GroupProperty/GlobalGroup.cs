using ConceptorUI.Models;

namespace ConceptorUI.ViewModels.Components.GroupProperty;

public class GlobalGroup
{
    public GroupProperties GetGlobalGroup()
    {
        return new GroupProperties
        {
            Name = GroupNames.Global.ToString(),
            Visibility = VisibilityValue.Visible.ToString(),
            Properties =
            [
                new Property
                {
                    Name = PropertyNames.SelectedMode.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = ESelectedMode.Single.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.FilePicker.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MoveLeft.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MoveRight.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MoveTop.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MoveBottom.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Focus.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "1",
                    Visibility = VisibilityValue.Collapsed.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MoveParentToChild.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.MoveChildToParent.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Trash.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = "0",
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.CanSelect.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = CanSelectValues.None.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                },

                new Property
                {
                    Name = PropertyNames.Screen.ToString(),
                    Type = PropertyTypes.Action.ToString(),
                    Value = CanSelectValues.None.ToString(),
                    Visibility = VisibilityValue.Visible.ToString()
                }
            ]
        };
    }
}