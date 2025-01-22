using System;
using System.IO;


namespace ConceptorUI.Constants
{
    class Env
    {
        public static readonly string DirBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string DirEnv = Path.Combine(DirBase, "UIConceptor");
        public static readonly string DirConfig = Path.Combine(DirEnv, "Configs");
        public static string DirProject = Path.Combine(DirEnv, "Projects");
        public static string DirMedia = Path.Combine(DirEnv, "Medias");

        public static readonly string FileConfig = Path.Combine(DirConfig, "config.json");
        public static readonly string FileScreen = Path.Combine(DirConfig, "screens_list.json");
    }
}
