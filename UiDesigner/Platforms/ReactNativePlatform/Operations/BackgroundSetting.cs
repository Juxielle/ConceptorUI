using ConceptorUI.Models;
using UiDesigner.Models;

namespace UiDesigner.Platforms.ReactNativePlatform.Operations;

static class BackgroundSetting
{
    public static void SetBackground(this RnComponent component, CompSerializer compSerializer)
    {
        var background = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.FillColor);
        
        if(background == ColorValue.Transparent.ToString())
            return;
        
        component.RnStyle.KeyValues.Add(new PlatformProperty { Key = "backgroundColor", Value = background });
    }
}