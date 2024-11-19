using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConceptorUI.Utils;

public class Helper
{

    public static double ConvertToDouble(string value)
    {
        double.TryParse(value.Replace(',', '.'), NumberStyles.Any, new CultureInfo("en-US"), out var newValue);
        return newValue;
    }

    public static string PickFile(bool openProject = false)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = "Download";
        dialog.DefaultExt = ".png";
        dialog.Filter = openProject ? "Zip files (*.zip;*.rar)|*.zip;*.rar" : "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

        var result = dialog.ShowDialog();

        return result == true ? dialog.FileName : string.Empty;
    }

    public static string SelectFolder()
    {
        var dlg = new Microsoft.Win32.SaveFileDialog();
        dlg.FileName = "application_ui";
        dlg.DefaultExt = ".xui";
        dlg.Filter = "Xui Applications (*.xui)|*.xui";

        var result = dlg.ShowDialog();
        
        return result == true ? dlg.FileName : null!;
    }

    public static void SaveBitmapImage(FrameworkElement control, string path)
    {
        var png = new PngBitmapEncoder();
            
        if (File.Exists(path)) return;
            
        var element = control as UIElement;
        double renderWidth, renderHeight;
        
        var width = renderWidth = element.RenderSize.Width;
        var height = renderHeight = element.RenderSize.Height;
                
        var rtb = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
        var visualBrush = new VisualBrush(element);
        var drawingVisual = new DrawingVisual();
            
        using (var drawingContext = drawingVisual.RenderOpen())
        {
            drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Point(width, height)));
        }
        rtb.Render(drawingVisual);

        png.Frames.Add(BitmapFrame.Create(rtb));
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            png.Save(stream);
        }
    }
    
    private static void CompressFolder(string folderName)
    {
        var path = Path.GetTempPath() + folderName;
        ZipFile.CreateFromDirectory(path, $"{path}.zip");
    }

    public static void SaveCompressedFolder(string folderName)
    {
        var dlg = new Microsoft.Win32.SaveFileDialog();
        dlg.FileName = folderName;
        dlg.DefaultExt = ".zip";
        dlg.Filter = "Zip documents (.zip)|*.zip";

        var result = dlg.ShowDialog();

        if (result != true) return;
        var destinationPath = dlg.FileName;
        var sourcePath = Path.GetTempPath() + folderName + ".zip";
        
        if(File.Exists(sourcePath))
            File.Move(sourcePath, destinationPath);
    }

    public static bool IsNumber(string value)
    {
        double.TryParse(value.Replace(',', '.'), NumberStyles.Any, new CultureInfo("en-US"), out var newValue);
        return (value != "0" || value != "0.0" || value != "0,0") && newValue != 0;
    }

    public static bool IsDeserializable<T>(string value)
    {
        try
        {
            var model = System.Text.Json.JsonSerializer.Deserialize<T>(value);
            return model != null;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static T Deserialize<T>(string value)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(value)!;
    }
}