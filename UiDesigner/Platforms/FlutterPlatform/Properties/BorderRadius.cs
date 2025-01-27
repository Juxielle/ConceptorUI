namespace UiDesigner.Platforms.FlutterPlatform.Properties;

public class BorderRadius : FtProperty
{
    public string TopLeft { get; init; }
    public string TopRight { get; init; }
    public string BottomLeft { get; init; }
    public string BottomRight { get; init; }
    public string UniformRadius { get; init; }
    public bool IsUniform { get; init; }
    
    public override string ToString(string space)
    {
        throw new System.NotImplementedException();
    }
}