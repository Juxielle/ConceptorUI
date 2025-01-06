using System.Collections.Generic;

namespace ConceptorUI.Platforms.ReactNativePlatform;

public class RnStyle
{
    public string Name { get; set; }
    public List<PlatformProperty> KeyValues { get; set; } = [];
}