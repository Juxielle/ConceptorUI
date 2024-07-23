using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptorUI.Constants
{
    class Env
    {
        public static string dirBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string dirEnv = $@"{dirBase}\UIConceptor";
        public static string dirConfig = $@"{dirEnv}\Configs";
        public static string dirProject = $@"{dirEnv}\Projects";
        public static string dirMedia = $@"{dirEnv}\Medias";

        public static string fileConfig = $@"{dirConfig}\config.json";

        public static string mediaFile(string fileName) {
            return $@"{dirMedia}\{fileName}";
        }
        public static string projectFolder(string folderName)
        {
            return $@"{dirProject}\{folderName}";
        }
        public static string configPFile(string pfolder, string fileName)
        {
            return $@"{dirProject}\{pfolder}\{fileName}";
        }
        public static string pemcFolder(string pfolder, string folderName)
        {
            return $@"{dirProject}\{pfolder}\{folderName}";
        }
        public static string pemcFile(string pfolder, string folderName, string fileName)
        {
            return $@"{dirProject}\{pfolder}\{folderName}\{fileName}";
        }
    }
}
