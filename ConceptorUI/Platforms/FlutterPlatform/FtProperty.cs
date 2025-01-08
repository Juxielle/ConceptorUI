namespace ConceptorUI.Platforms.FlutterPlatform;

public abstract class FtProperty
{
    public string Name { get; set; }

    public abstract string ToString(string space);
}