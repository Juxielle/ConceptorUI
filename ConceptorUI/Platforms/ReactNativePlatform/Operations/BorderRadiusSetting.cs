using ConceptorUI.Models;
using ConceptorUI.Utils;

namespace ConceptorUI.Platforms.ReactNativePlatform.Operations;

static class BorderRadiusSetting
{
    public static void SetBorderRadius(this RnComponent component, CompSerializer compSerializer)
    {
        var borderRadEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CBorderRadius);
        if (borderRadEnable == "1")
        {
            var borderRad = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderRadius);
            component.BorderRadius = Helper.FormatString(borderRad);

            var borderRadValue = Helper.ConvertToDouble(borderRad);
            if (borderRadValue == 0) return;

            component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "borderRadius", Value = component.BorderRadius });
        }
        else
        {
            /* Radius TopLeft */
            var borderRadTopLeft = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusTopLeft);
            component.BorderTopLeftRadius = Helper.FormatString(borderRadTopLeft);

            var borderRadTopLeftValue = Helper.ConvertToDouble(borderRadTopLeft);
            if (borderRadTopLeftValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderTopLeftRadius", Value = component.BorderTopLeftRadius });

            /* Radius TopRight */
            var borderRadTopRight = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusTopRight);
            component.BorderTopRightRadius = Helper.FormatString(borderRadTopRight);

            var borderRadTopRightValue = Helper.ConvertToDouble(borderRadTopRight);
            if (borderRadTopRightValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderTopRightRadius", Value = component.BorderTopRightRadius });

            /* Radius BottomLeft */
            var borderRadBottomLeft = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusBottomLeft);
            component.BorderBottomLeftRadius = Helper.FormatString(borderRadBottomLeft);

            var borderRadBottomLeftValue = Helper.ConvertToDouble(borderRadBottomLeft);
            if (borderRadBottomLeftValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderBottomLeftRadius", Value = component.BorderBottomLeftRadius });

            /* Radius BottomRight */
            var borderRadBottomRight = compSerializer.GetGroup(GroupNames.Appearance)
                .GetValue(PropertyNames.BorderRadiusBottomRight);
            component.BorderBottomRightRadius = Helper.FormatString(borderRadBottomRight);

            var borderRadBottomRightValue = Helper.ConvertToDouble(borderRadBottomRight);
            if (borderRadBottomRightValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderBottomRightRadius", Value = component.BorderBottomRightRadius });
        }
    }
}