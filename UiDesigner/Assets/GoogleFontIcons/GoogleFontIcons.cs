using System.Collections.Generic;
using System.Windows.Controls;
using UiDesigner.Models;

namespace UiDesigner.Assets.GoogleFontIcons;

public class GoogleFontIcons
{
    
    public static GoogleFontIconSingle GetIcon(string name)
    {
        var icons = GetIcons();
        
        return icons.Find(icon => icon.Name == name)!;
    }
    
    public static List<GoogleFontIconSingle> GetIcons()
    {
        return [
            new GoogleFontIconSingle
            {
                Name = "Home",
                Icon = new Home()
            },
            new GoogleFontIconSingle
            {
                Name = "Setting",
                Icon = new Setting()
            },
        ];
    }

    public static IconPack GetIconPack()
    {
        return new IconPack
        {
            Label = "Google font icons",
            Version = "1.0",
            IsFont = false,
            Icons = [
                new Icon { Name = "Home", Code = "Home" },
                new Icon { Name = "Setting", Code = "Setting" },
            ]
        };
    }
}

public record GoogleFontIconSingle
{
    public string Name { get; set; }
    public TextBlock Icon { get; set; }
}