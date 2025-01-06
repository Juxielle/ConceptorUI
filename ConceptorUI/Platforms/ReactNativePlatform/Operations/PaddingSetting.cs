using ConceptorUI.Models;
using ConceptorUI.Utils;

namespace ConceptorUI.Platforms.ReactNativePlatform.Operations;

static class PaddingSetting
{
    public static void SetPadding(this RnComponent component, CompSerializer compSerializer)
    {
        var paddingEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CPadding);
        if (paddingEnable == "1")
        {
            var padding = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.Padding);
            component.Padding = Helper.FormatString(padding);

            var paddingValue = Helper.ConvertToDouble(padding);
            if (paddingValue == 0) return;

            component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "padding", Value = component.Padding });
        }
        else
        {
            var paddingLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingLeft);
            var paddingRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingRight);
            var paddingTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingTop);
            var paddingBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.PaddingBottom);
            component.Padding = $"{Helper.FormatString(paddingTop)}px " +
                                $"{Helper.FormatString(paddingRight)}px " +
                                $"{Helper.FormatString(paddingLeft)}px " +
                                $"{Helper.FormatString(paddingBottom)}px";

            var paddingLeftValue = Helper.ConvertToDouble(paddingLeft);
            var paddingRightValue = Helper.ConvertToDouble(paddingRight);
            var paddingTopValue = Helper.ConvertToDouble(paddingTop);
            var paddingBottomValue = Helper.ConvertToDouble(paddingBottom);
            if (paddingLeftValue == 0 && paddingRightValue == 0 &&
                paddingTopValue == 0 && paddingBottomValue == 0) return;

            component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "padding", Value = component.Padding });
        }
    }
}