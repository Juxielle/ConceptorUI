using System.Collections.Generic;
using ConceptorUI.Constants;


namespace ConceptorUI.Models
{
    public class Icon
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontPath { get; set; } = $"{Env.DirEnv}/Icons/MaterialIcons-Regular.ttf#Material Icons";
    }

    public class IconPack
    {
        public string Label { get; set; }
        public bool IsFont { get; set; }
        public string Version { get; set; }
        public List<Icon> Icons { get; set; }
    }
}
