using System.Collections.Generic;

namespace ConceptorUI.Platforms.ReactNativePlatform;

public abstract class RnComponent : PlatformComponent
{
    public string AlignSelf { get; set; }
    public string Background { get; set; }
    public string Flex { get; set; }

    public string Border { get; set; }
    public string BorderLeft { get; set; }
    public string BorderRight { get; set; }
    public string BorderTop { get; set; }
    public string BorderBottom { get; set; }

    public string Margin { get; set; }

    public string Padding { get; set; }

    public string BorderRadius { get; set; }
    public string BorderBottomLeftRadius { get; set; }
    public string BorderBottomRightRadius { get; set; }
    public string BorderTopLeftRadius { get; set; }
    public string BorderTopRightRadius { get; set; }

    public List<string> Styles { get; init; }
    public RnStyle RnStyle = new();

    public override string ToString(string space)
    {
        return ConvertToString(space);
    }

    protected abstract string ConvertToString(string space);
}