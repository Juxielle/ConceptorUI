using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.ReusableComponent;

static class ComponentRestoreProperties
{
    public static void RestoreProperties(this ComponentModel component)
    {
        var isHorizontal = Alignment.IsHorizontal(component);

        var isVertical = Alignment.IsVertical(component);

        Alignment.SetSeveralActivations(component);
        Alignment.SetInvalidValues(component);

        if (isHorizontal)
        {
            Alignment.SetHorizontalOnNull(component);
        }

        if (isVertical)
        {
            Alignment.SetVerticalOnNull(component);
        }

        /* Body */
        var bodyW = component.Body.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
        var bodyH = component.Body.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);

        if (bodyW != SizeValue.Expand.ToString() && bodyW != SizeValue.Auto.ToString())
        {
            component.Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
        }

        if (bodyH != SizeValue.Expand.ToString() && bodyH != SizeValue.Auto.ToString())
        {
            component.Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "100");
        }

        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(component);
        SelfAlignment.SetInvalidValues(component);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(component);
        var isSelfVertical = SelfAlignment.IsVertical(component);

        var height = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            SelfAlignment.SetHorizontalOnNull(component);
        }

        if (isSelfVertical)
        {
            SelfAlignment.SetVerticalOnNull(component);
        }

        if (width == SizeValue.Expand.ToString())
        {
            component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString());
        }

        if (height == SizeValue.Expand.ToString())
        {
            component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString());
        }

        component.Synchronize();
    }
}