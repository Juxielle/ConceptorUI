using ConceptorUI.Models;

namespace ConceptorUI.Platforms.ReactNativePlatform.Operations;

static class PaddingSetting
{
    public static void SetPadding(this RnComponent component, CompSerializer compSerializer)
    {
        var paddingEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CPadding);
        if (paddingEnable == "1")
        {
            var padding = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.Padding);
            component.Padding = padding.Replace(",", ".");
        }
        else
        {
            var paddingLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingLeft);
            var paddingRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingRight);
            var paddingTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingTop);
            var paddingBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingBottom);
            component.Padding = $"{paddingTop}px " +
                                $"{paddingRight}px " +
                                $"{paddingLeft}px " +
                                $"{paddingBottom}px";
        }
    }
}