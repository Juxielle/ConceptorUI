namespace ConceptorUI.Platforms.FlutterPlatform;

public abstract class FtComponent : PlatformComponent
{
    public override string ToString(string space)
    {
        return ConvertToString(space);
    }

    protected abstract string ConvertToString(string space);
}