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

        var height = icon.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = icon.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (width == SizeValue.Expand.ToString() || width == SizeValue.Auto.ToString())
        {
            icon.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "20");
        }

        if (height == SizeValue.Expand.ToString() || height == SizeValue.Auto.ToString())
        {
            icon.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "20");
        }

        icon.Synchronize();
    }
}