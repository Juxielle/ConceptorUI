using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using ConceptorUI.Constants;

namespace ConceptorUI.Services;

public class ReadImageFromZipService
{
    public static BitmapImage GetImage(string zipPath, string projectName, string imageName)
    {
        try
        {
            Console.WriteLine($"Image path: {projectName}/Medias/{imageName}");
            using var archive = ZipFile.OpenRead(zipPath);
            var mediaEntry = archive.GetEntry($"{projectName}/Medias/");
            var filePath = Path.Combine(Env.DirEnv, $"Medias/{imageName}");

            if (mediaEntry == null)
                throw new Exception();
            //Chaque application possède son cache dans le Temp folder.
            //Pour cela, une application doit avoir un identifiant unique
            //Cet id lui est attribué lors du lancement

            if(!File.Exists(filePath))
                mediaEntry.ExtractToFile(Env.DirEnv, overwrite: true);
            
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.EndInit();

            return bitmap;
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Assets/image.png", UriKind.Absolute);
            bitmap.EndInit();
            
            return bitmap;
        }
    }
}