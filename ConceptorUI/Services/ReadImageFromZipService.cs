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
            var entry = archive.GetEntry($"{projectName}/Medias/{imageName}");
            var mediaEntry = archive.GetEntry($"{projectName}/Medias");
            var temp = Path.GetTempPath();

            if (entry == null)
                throw new Exception();

            using var stream = entry.Open();
            //stream.Position = 0;
            mediaEntry?.ExtractToFile(Env.DirEnv, overwrite: true);
            
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            //bitmap.Freeze();

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