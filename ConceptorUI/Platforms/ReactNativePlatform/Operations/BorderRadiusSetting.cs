using ConceptorUI.Models;

namespace ConceptorUI.Platforms.ReactNativePlatform.Operations;

static class BorderRadiusSetting
{
    public static void SetBorderRadius(this RnComponent component, CompSerializer compSerializer)
    {
        var borderRadEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CBorderRadius);
        if (borderRadEnable == "1")
        {
            var borderRad = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderRadius);
            component.BorderRadius = borderRad.Replace(",", ".");
        }
        else
        {
            var borderRadTopLeft = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusTopLeft);
            component.BorderTopLeftRadius = borderRadTopLeft.Replace(",", ".");

            var borderRadTopRight = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusTopRight);
            component.BorderTopRightRadius = borderRadTopRight.Replace(",", ".");

            var borderRadBottomLeft = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusBottomLeft);
            component.BorderBottomLeftRadius = borderRadBottomLeft.Replace(",", ".");

            var borderRadBottomRight = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusBottomRight);
            component.BorderBottomRightRadius = borderRadBottomRight.Replace(",", ".");
        }
    }
}