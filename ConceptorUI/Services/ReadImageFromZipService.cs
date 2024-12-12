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
            using var archive = ZipFile.OpenRead(zipPath);
            var mediaEntry = archive.GetEntry($"{projectName}/Medias/{imageName}");
            var filePath = Path.Combine(Env.DirEnv, $"Medias/{imageName}");
            
            var tempPath = Path.Combine(Path.GetTempPath(), imageName);
            
            if (mediaEntry == null)
                throw new Exception();
            
            if(!File.Exists(filePath))
            {
                mediaEntry.ExtractToFile(tempPath, overwrite: true);
                File.Copy(tempPath, filePath, true);
            }
            
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.EndInit();
            
            return bitmap;
        }
        catch (Exception e)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Assets/image.png", UriKind.Absolute);
            bitmap.EndInit();
            
            return bitmap;
        }
    }
}