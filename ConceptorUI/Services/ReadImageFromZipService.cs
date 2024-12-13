using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using ConceptorUI.Constants;

namespace ConceptorUI.Services;

public class ReadImageFromZipService
{
    public static BitmapImage GetImage(string zipPath, string projectId, string projectTempId, string imageName)
    {
        try
        {
            var projectMediaPath = Path.Combine(Env.DirEnv, $"Medias/{projectTempId}");
            var filePath = Path.Combine(projectMediaPath, imageName);
                
            if (!Directory.Exists(projectMediaPath))
                Directory.CreateDirectory(projectMediaPath);
            if (File.Exists(filePath))
                return GetBitmap(filePath);
            
            using var archive = ZipFile.OpenRead(zipPath);
            var mediaEntry = archive.GetEntry($"{projectId}/Medias/{imageName}");
            
            var tempPath = Path.Combine(Path.GetTempPath(), imageName);
            
            if (mediaEntry == null)
                throw new Exception();
            
            if(!File.Exists(filePath))
            {
                mediaEntry.ExtractToFile(tempPath, overwrite: true);
                File.Copy(tempPath, filePath, true);
                /*
                 * Enregistrer l'image dans un dossier spécifique à l'application si elle n'n'existe pas
                 * Inventer une procédure de suppression du dossier
                 */
            }
            
            return GetBitmap(filePath);
        }
        catch (Exception)
        {
            return GetBitmap("pack://application:,,,/Assets/image.png");
        }
    }

    private static BitmapImage GetBitmap(string path)
    {
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.UriSource = new Uri(path, UriKind.Absolute);
        bitmap.EndInit();
            
        return bitmap;
    }
}