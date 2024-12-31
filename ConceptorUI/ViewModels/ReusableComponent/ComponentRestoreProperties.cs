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
        
        /* Content */
        

        /* Status Bar */
        

        /* Body */

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
        
        if (width != "300")
        {
            component.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
        }
        
        if (height != "620")
        {
            component.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "620");
        }
        
        component.Synchronize();
    }
}