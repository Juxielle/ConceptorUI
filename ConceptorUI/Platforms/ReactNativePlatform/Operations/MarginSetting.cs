using ConceptorUI.Models;

namespace ConceptorUI.Platforms.ReactNativePlatform.Operations;

static class MarginSetting
{
    public static void SetMargin(this RnComponent component, CompSerializer compSerializer)
    {
        var marginEnable = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.CMargin);
        if (marginEnable == "1")
        {
            var margin = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.Margin);
            component.Margin = margin.Replace(",", ".");
        }
        else
        {
            var marginLeft = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginLeft);
            var marginRight = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginRight);
            var marginTop = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginTop);
            var marginBottom = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.MarginBottom);
            component.Margin = $"{marginTop}px " +
                               $"{marginRight}px " +
                               $"{marginLeft}px " +
                               $"{marginBottom}px";
        }
    }
}