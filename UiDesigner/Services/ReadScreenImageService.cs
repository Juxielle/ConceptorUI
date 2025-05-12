using System;
using System.IO;
using System.Windows.Media.Imaging;
using UiDesigner.Constants;

namespace ConceptorUI.Services;

public abstract class ReadScreenImageService
{
    public static BitmapImage GetImage(string imageName)
    {
        try
        {
            var projectMediaPath = Path.Combine(Env.DirEnv, $"Medias/Screens");
            var filePath = Path.Combine(projectMediaPath, imageName);
            var tempPath = Path.Combine(Path.GetTempPath(), imageName);
            
            if (File.Exists(tempPath))
                return GetBitmap(tempPath);
            
            File.Copy(filePath, tempPath, true);
            
            return GetBitmap(tempPath);
        }
        catch (Exception)
        {
            return GetBitmap("pack://application:,,,/Assets/screen11.png");
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