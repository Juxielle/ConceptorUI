using ConceptorUi.ViewModels.Operations;
using ConceptorUI.ViewModels.ReusableComponent;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.ReusableComponent;

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
        var isBodyHorizontal = Alignment.IsHorizontal(component.Body);
        var isBodyVertical = Alignment.IsVertical(component.Body);

        Alignment.SetSeveralActivations(component.Body);
        Alignment.SetInvalidValues(component.Body);

        if (isBodyHorizontal)
        {
            Alignment.SetHorizontalOnNull(component.Body);
        }

        if (isBodyVertical)
        {
            Alignment.SetVerticalOnNull(component.Body);
        }

        SelfAlignment.SetSeveralActivations(component.Body);
        SelfAlignment.SetInvalidValues(component.Body);

        if (bodyW != SizeValue.Expand.ToString() && bodyW != SizeValue.Auto.ToString())
        {
            component.Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
        }

        if (bodyH != SizeValue.Expand.ToString() && bodyH != SizeValue.Auto.ToString())
        {
            component.Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "100");
        }

        component.Body.Synchronize();

        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(component);
        SelfAlignment.SetInvalidValues(component);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(component);
        var isSelfVertical = SelfAlignment.IsVertical(component);

        var height = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = component.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (!isSelfHorizontal)
        {
            SelfAlignment.SetHorizontalValue(component, PropertyNames.Hc, "1");
        }

        if (isSelfVertical)
        {
            SelfAlignment.SetVerticalOnNull(component);
        }

        if (width != SizeValue.Auto.ToString())
        {
            component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
        }

        if (height != SizeValue.Auto.ToString())
        {
            component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
        }

        component.Synchronize();
    }
}