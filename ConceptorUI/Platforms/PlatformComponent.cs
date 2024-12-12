using System.Collections.Generic;

namespace ConceptorUI.Platforms;

public abstract class PlatformComponent
{
    public string Name { get; init; }
    public string Package { get; init; }
    public string MatchComponentName { get; init; }
    public List<PlatformProperty> Properties { get; init; }
    public List<string> Children { get; init; }

    public new abstract string ToString();
}