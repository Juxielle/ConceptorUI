using UiDesigner.Models;
using UiDesigner.Utils;

namespace UiDesigner.Platforms.ReactNativePlatform.Operations;

static class MarginSetting
{
    public static void SetMargin(this RnComponent component, CompSerializer compSerializer)
    {
        var marginEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CMargin);
        if (marginEnable == "1")
        {
            var margin = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.Margin);
            component.Margin = margin.Replace(",", ".");

            var marginValue = Helper.ConvertToDouble(margin);
            if (marginValue == 0) return;

            component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "margin", Value = component.Margin });
        }
        else
        {
            var marginLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginLeft);
            var marginRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginRight);
            var marginTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginTop);
            var marginBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginBottom);
            
            component.Margin = $"{Helper.FormatString(marginTop)}px " +
                               $"{Helper.FormatString(marginRight)}px " +
                               $"{Helper.FormatString(marginLeft)}px " +
                               $"{Helper.FormatString(marginBottom)}px";

            var marginLeftValue = Helper.ConvertToDouble(marginLeft);
            var marginRightValue = Helper.ConvertToDouble(marginRight);
            var marginTopValue = Helper.ConvertToDouble(marginTop);
            var marginBottomValue = Helper.ConvertToDouble(marginBottom);
            
            if (marginLeftValue == 0 && marginRightValue == 0 &&
                marginTopValue == 0 && marginBottomValue == 0) return;

            component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "margin", Value = component.Margin });
        }
    }
}