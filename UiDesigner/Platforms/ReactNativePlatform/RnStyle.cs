using System.Collections.Generic;

namespace UiDesigner.Platforms.ReactNativePlatform;

public class RnStyle
{
    public string Name { get; set; }
    public List<PlatformProperty> KeyValues { get; set; } = [];
}