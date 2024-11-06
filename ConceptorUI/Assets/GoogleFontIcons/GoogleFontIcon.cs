using System.Collections.Generic;
using System.Windows.Controls;

namespace ConceptorUI.Assets.GoogleFontIcons;

public class GoogleFontIcon
{
    
    public static TextBlock GetIcon(string name)
    {
        var icons = GetIcons();
        
        return icons.Find(icon => icon.Name == name)?.Icon!;
    }
    
    public static List<GoogleFontIconSingle> GetIcons()
    {
        return [];
    }
}

public record GoogleFontIconSingle
{
    public string Name { get; set; }
    public TextBlock Icon { get; set; }
}