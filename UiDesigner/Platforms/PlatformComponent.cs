using System.Collections.Generic;

namespace UiDesigner.Platforms;

public abstract class PlatformComponent
{
    public string Name { get; init; }
    public string Package { get; init; }
    public bool IsMainInPackage { get; init; }
    public string MatchComponentName { get; init; }
    public List<PlatformProperty> Properties { get; init; }
    public List<PlatformComponent> Children { get; init; }

    public new abstract string ToString(string space);
}