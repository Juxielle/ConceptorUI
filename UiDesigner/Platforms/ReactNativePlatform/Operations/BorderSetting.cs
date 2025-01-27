using UiDesigner.Models;
using UiDesigner.Utils;

namespace UiDesigner.Platforms.ReactNativePlatform.Operations;

static class BorderSetting
{
    public static void SetBorder(this RnComponent component, CompSerializer compSerializer)
    {
        var borderEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CBorder);
        var borderColor = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderColor);
        if (borderEnable == "1")
        {
            var borderWidth = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderWidth);
            component.Border = $"{borderWidth}px " +
                               $"solid " +
                               $"{borderColor}";

            var borderWidthValue = Helper.ConvertToDouble(borderWidth);
            if (borderWidthValue == 0) return;

            component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "border", Value = component.Border });
        }
        else
        {
            /* Boder Left */
            var borderLeftWidth =
                compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderLeftWidth);
            component.BorderLeft = $"{borderLeftWidth}px " +
                                   $"solid " +
                                   $"{borderColor}";

            var borderLeftWidthValue = Helper.ConvertToDouble(borderLeftWidth);
            if (borderLeftWidthValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderLeft", Value = component.BorderLeft });

            /* Boder Right */
            var borderRightWidth =
                compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderRightWidth);
            component.BorderRight = $"{borderRightWidth}px " +
                                    $"solid " +
                                    $"{borderColor}";

            var borderRightWidthValue = Helper.ConvertToDouble(borderRightWidth);
            if (borderRightWidthValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderRight", Value = component.BorderRight });

            /* Boder Top */
            var borderTopWidth = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderTopWidth);
            component.BorderTop = $"{borderTopWidth}px " +
                                  $"solid " +
                                  $"{borderColor}";

            var borderTopWidthValue = Helper.ConvertToDouble(borderTopWidth);
            if (borderTopWidthValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderTop", Value = component.BorderTop });

            /* Boder Bottom */
            var borderBottomWidth =
                compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderBottomWidth);
            component.BorderBottom = $"{borderBottomWidth}px " +
                                     $"solid " +
                                     $"{borderColor}";

            var borderBottomWidthValue = Helper.ConvertToDouble(borderBottomWidth);
            if (borderBottomWidthValue != 0)
                component.RnStyle.KeyValues.Add(new PlatformProperty
                    { Key = "borderBottom", Value = component.BorderBottom });
        }
    }
}