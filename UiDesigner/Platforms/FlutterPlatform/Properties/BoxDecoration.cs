namespace UiDesigner.Platforms.FlutterPlatform.Properties;

public class BoxDecoration : FtProperty
{
    public string Color { get; set; }
    public BorderRadius BorderRadius { get; set; }
    public BoxShadow BoxShadow { get; set; }
    
    public override string ToString(string space)
    {
        throw new System.NotImplementedException();
    }
}