using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.Image;

static class ImageRestoreProperties
{
    public static void RestoreProperties(this ImageModel image)
    {
        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(image);
        SelfAlignment.SetInvalidValues(image);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(image);
        var isSelfVertical = SelfAlignment.IsVertical(image);

        var height = image.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = image.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            if (width == SizeValue.Expand.ToString())
            {
                image.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
            }
        }

        if (isSelfVertical)
        {
            if (height == SizeValue.Expand.ToString())
            {
                image.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            }
        }

        image.Synchronize();
    }
}