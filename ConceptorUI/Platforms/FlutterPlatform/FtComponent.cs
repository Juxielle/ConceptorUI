using System.Collections.Generic;
using ConceptorUI.Models;

namespace ConceptorUI.Platforms.FlutterPlatform;

public abstract class FtComponent : PlatformComponent
{
    protected List<string> Styles = [];
    public readonly List<PlatformProperty> FtStyles = [];
    public string Space;
    
    public override string ToString(string space)
    {
        return ConvertToString(space);
    }
    
    protected abstract string ConvertToString(string space);
    protected abstract void Build(CompSerializer compSerializer);
    protected abstract void BuildSingle(FtComponent child);
}