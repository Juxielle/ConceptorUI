using ConceptorUI.Models;
using ConceptorUI.ViewModels.Image;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace UiDesigner.ViewModels.Image;

static class ImageRestoreProperties
{
    public static void RestoreProperties(this ImageModel image)
    {
        /* SelfAlignment */
        SelfAlignment.SetSeveralActivations(image);
        SelfAlignment.SetInvalidValues(image);

        var height = image.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = image.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (width == SizeValue.Auto.ToString())
        {
            image.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "40");
        }

        if (height == SizeValue.Auto.ToString())
        {
            image.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "40");
        }

        image.Synchronize();
    }
}