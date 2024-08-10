using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using ConceptorUI.Constants;


namespace ConceptorUI.Models
{
    internal class Icons
    {
        public string? Name { get; set; }
        public PackIcon? Icon { get; set; }
        public string? StringIcon { get; set; }
        public List<IconP>? IconPlateform { get; set; }
    }

    public abstract class IconP
    {
        public string? Library { get; set; }
        public string? Component { get; set; }
        public bool? IsDefault { get; set; } = true;
        public string? Name { get; set; }
    }

    public class Icon
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontPath { get; set; } = $"{Env.dirEnv}/Icons/MaterialIcons-Regular.ttf#Material Icons";
    }

    public class IconPack
    {
        public string Label { get; set; }
        public bool IsFont { get; set; } = false;
        public string Version { get; set; }
        public List<Icon> Icons { get; set; }
    }
}
