using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.Window;

static class WindowRestoreProperties
{
    public static void RestoreProperties(this WindowModel window)
    {
        var isHorizontal = Alignment.IsHorizontal(window);

        var isVertical = Alignment.IsVertical(window);

        Alignment.SetSeveralActivations(window);
        Alignment.SetInvalidValues(window);

        if (isHorizontal)
        {
            Alignment.SetHorizontalOnNull(window);
        }

        if (isVertical)
        {
            Alignment.SetVerticalOnNull(window);
        }
        
        /* Content */
        
        
        /* Status Bar */
        
        
        /* Body */

        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(window);
        SelfAlignment.SetInvalidValues(window);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(window);
        var isSelfVertical = SelfAlignment.IsVertical(window);

        var height = window.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = window.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            SelfAlignment.SetHorizontalOnNull(window);
        }

        if (isSelfVertical)
        {
            SelfAlignment.SetVerticalOnNull(window);
        }

        if (width != "300")
        {
            window.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
        }

        if (height != "620")
        {
            window.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "620");
        }

        window.Synchronize();
    }
}