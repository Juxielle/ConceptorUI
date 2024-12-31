using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.Icon;

static class IconRestoreProperties
{
    public static void RestoreProperties(this IconModel icon)
    {
        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(icon);
        SelfAlignment.SetInvalidValues(icon);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(icon);
        var isSelfVertical = SelfAlignment.IsVertical(icon);

        var height = icon.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = icon.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            if (width == SizeValue.Expand.ToString())
            {
                icon.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
            }
        }

        if (isSelfVertical)
        {
            if (height == SizeValue.Expand.ToString())
            {
                icon.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            }
        }

        icon.Synchronize();
    }
}