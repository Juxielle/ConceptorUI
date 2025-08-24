using ConceptorUI.Models;
using ConceptorUi.ViewModels.Operations;

namespace ConceptorUI.ViewModels.Text;

static class TextRestoreProperties
{
    public static void RestoreProperties(this TextModel text)
    {
        SelfAlignment.SetSeveralActivations(text);
        SelfAlignment.SetInvalidValues(text);

        var isSelfHorizontal = SelfAlignment.IsHorizontal(text);
        var isSelfVertical = SelfAlignment.IsVertical(text);

        var height = text.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
        var width = text.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

        if (isSelfHorizontal)
        {
            if (width == SizeValue.Expand.ToString())
            {
                text.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString());
            }
        }

        if (isSelfVertical)
        {
            if (height == SizeValue.Expand.ToString())
            {
                text.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString());
            }
        }

        text.Synchronize();
    }
}