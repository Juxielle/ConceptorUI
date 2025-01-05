using System.Collections.Generic;

namespace ConceptorUI.Platforms.ReactNativePlatform;

public abstract class RnComponent : PlatformComponent
{
    public string AlignSelf { get; set; }
    public string Background { get; set; }
    public string Flex { get; set; }
    public string Border { get; set; }
    public string Margin { get; set; }
    public string Padding { get; set; }
    
    public List<string> Styles { get; init; }
    
    public override string ToString(string space)
    {
        return string.Empty;
    }

    public abstract RnStyle BuildStyle(int index);
}