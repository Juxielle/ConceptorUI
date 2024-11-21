using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ConceptorUI.Classes;
using ConceptorUI.Constants;

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
    
    public static void CompressFolder(string folderPath, string filePath)
    {
        var folderName = folderPath.Replace(Path.GetDirectoryName(folderPath)+@"\", "");
        var newPath = folderPath + "_copy";
        var targetFolderPath = Path.Combine(newPath, folderName);
        CopyDirectory(folderPath, targetFolderPath, true);
        
        if(File.Exists(filePath))
            File.Delete(filePath);
        ZipFile.CreateFromDirectory(newPath, filePath);
        Directory.Delete(newPath, true);
    }

    public static List<Project> GetProjects()
    {
        var projects = JsonSerializer.Deserialize<List<Project>>(
            File.ReadAllText(Env.FileConfig)
        );
        return projects!;
    }

    public static bool SaveProjects(Project project)
    {
        var projects = JsonSerializer.Deserialize<List<Project>>(
            File.ReadAllText(Env.FileConfig)
        );
        // var find = projects.Find();
        //
        // var jsonString = JsonSerializer.Serialize(componentSerializer);
        // File.WriteAllText(filePath, jsonString);
        return false;
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
    
    private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        var dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        var dirs = dir.GetDirectories();

        Directory.CreateDirectory(destinationDir);

        foreach (var file in dir.GetFiles())
        {
            var targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        if (!recursive) return;
        foreach (var subDir in dirs)
        {
            var newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
}