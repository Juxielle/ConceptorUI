using System.Linq;
using ConceptorUI.Models;
using ConceptorUI.Platforms.ReactNativePlatform.Operations;

namespace ConceptorUI.Platforms.ReactNativePlatform.Components;

public class RnView : RnComponent
{
    private string AlignItems { get; set; }
    private string JustifyContent { get; set; }
    private string FlexDirection { get; set; }

    public RnView(CompSerializer compSerializer)
    {
        var hl = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.Hl);
        var hc = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.Hc);
        var hr = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.Hr);

        var vt = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.Vt);
        var vc = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.Vc);
        var vb = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.Vb);

        var sb = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
        var sa = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
        var se = compSerializer.GetGroup(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

        if (compSerializer.Name == ComponentList.Row.ToString())
        {
            FlexDirection = "column";

            JustifyContent = vt == "1" ? "flex-start" :
                vc == "1" ? "center" :
                vb == "1" ? "flex-end" :
                sb == "1" ? "space-between" :
                sa == "1" ? "space-around" :
                se == "1" ? "space-every" : "flex-start";

            AlignItems = hl == "1" ? "start" :
                hc == "1" ? "center" :
                hr == "1" ? "end" : "stretch";
            
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "flexDirection", Value = FlexDirection });
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "justifyContent", Value = JustifyContent });
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "alignItems", Value = AlignItems });
        }
        else if (compSerializer.Name == ComponentList.Column.ToString())
        {
            FlexDirection = "row";

            JustifyContent = hl == "1" ? "flex-start" :
                hc == "1" ? "center" :
                hr == "1" ? "flex-end" :
                sb == "1" ? "space-between" :
                sa == "1" ? "space-around" :
                se == "1" ? "space-every" : "flex-start";

            AlignItems = vt == "1" ? "start" :
                vc == "1" ? "center" :
                vb == "1" ? "end" : "stretch";
            
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "flexDirection", Value = FlexDirection });
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "justifyContent", Value = JustifyContent });
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "alignItems", Value = AlignItems });
        }
        else if (compSerializer.Name == ComponentList.Container.ToString())
        {
            JustifyContent = vt == "1" ? "flex-start" :
                vc == "1" ? "center" :
                vb == "1" ? "flex-end" : "flex-start";

            AlignItems = hl == "1" ? "start" :
                hc == "1" ? "center" :
                hr == "1" ? "end" : "stretch";
            
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "justifyContent", Value = JustifyContent });
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "alignItems", Value = AlignItems });
        }

        this.SetBackground(compSerializer);
        this.SetMargin(compSerializer);
        this.SetPadding(compSerializer);
        this.SetBorder(compSerializer);
        this.SetBorderRadius(compSerializer);
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
        RnStyle.Name = $"view-style{index}";
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
                new PlatformProperty { Key = "margin", Value = Margin },
                new PlatformProperty { Key = "padding", Value = Padding },
                new PlatformProperty { Key = "border", Value = Border },
            ]
        };
    }
}