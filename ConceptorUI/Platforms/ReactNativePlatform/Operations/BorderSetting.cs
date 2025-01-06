using ConceptorUI.Models;

namespace ConceptorUI.Platforms.ReactNativePlatform.Operations;

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
        }
        else
        {
            var borderLeftWidth =
                compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderLeftWidth);
            component.BorderLeft = $"{borderLeftWidth}px " +
                                   $"solid " +
                                   $"{borderColor}";

            var borderRightWidth =
                compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderRightWidth);
            component.BorderRight = $"{borderRightWidth}px " +
                                    $"solid " +
                                    $"{borderColor}";

            var borderTopWidth = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderTopWidth);
            component.BorderTop = $"{borderTopWidth}px " +
                                  $"solid " +
                                  $"{borderColor}";

            var borderBottomWidth =
                compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.BorderBottomWidth);
            component.BorderBottom = $"{borderBottomWidth}px " +
                                     $"solid " +
                                     $"{borderColor}";
        }
    }
}