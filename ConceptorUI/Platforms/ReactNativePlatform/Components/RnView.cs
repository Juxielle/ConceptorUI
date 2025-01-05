using System.Linq;
using ConceptorUI.Models;

namespace ConceptorUI.Platforms.ReactNativePlatform.Components;

public class RnView : RnComponent
{
    public string AlignItems { get; set; }
    public string JustifyContent { get; set; }
    public string FlexDirection { get; set; }

    public RnView(CompSerializer compSerializer)
    {
        Background = compSerializer.GetGroup(GroupNames.Appearance).GetValue(PropertyNames.FillColor);
        Background = Background == ColorValue.Transparent.ToString()
            ? ColorValue.Transparent.ToString().ToLower()
            : Background;

        if (compSerializer.Name == ComponentList.Row.ToString())
        {
            FlexDirection = "column";
        }
    }

    public override string ToString(string space)
    {
        var styles = string.Join(",", Styles);

        return space + @$"<View style=""[{styles}]"">" + "\n" +
               $"{Children.Select(c => c.ToString($"{space}{Platform.WhiteSpace}"))}\n" +
               space + "</View>";
    }

    public override RnStyle BuildStyle(int index)
    {
        return new RnStyle
        {
            Name = $"view-style{index}",
            KeyValues =
            [
                new PlatformProperty { Key = "background", Value = Background },
                new PlatformProperty { Key = "flex", Value = Flex },
                new PlatformProperty { Key = "flexDirection", Value = FlexDirection },
                new PlatformProperty { Key = "alignSelf", Value = AlignSelf },
                new PlatformProperty { Key = "alignItems", Value = AlignItems },
                new PlatformProperty { Key = "justifyContent", Value = JustifyContent },
            ]
        };
    }
}