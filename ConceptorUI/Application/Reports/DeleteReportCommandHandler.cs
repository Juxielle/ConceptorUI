using System;
using System.IO.Compression;
using System.Threading.Tasks;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Reports;

public class DeleteReportCommandHandler
{
    public Task<Result<bool>> Handle(DeleteReportCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            var entry = archive.GetEntry($@"{command.ProjectName}/Pages/{command.FileCode}.json");
            entry?.Delete();

            return Task.FromResult(Result<bool>.Success(true));
        }
        catch (Exception)
        {
            return Task.FromResult(Result<bool>.Failure(Error.NotFound));
        }
    }
}

/* => Lire une image depuis un fichier zip: Utiliser un service pour ça.
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO.Compression;

namespace ZipImageViewer
{
   public partial class MainWindow : Window
   {
       public MainWindow()
       {
           InitializeComponent();
           LoadImageFromZip("chemin/vers/votre/fichier.zip", "nom_de_l_image.jpg");
       }

       private void LoadImageFromZip(string zipPath, string imageName)
       {
           using (ZipArchive archive = ZipFile.OpenRead(zipPath))
           {
               var entry = archive.GetEntry(imageName);
               if (entry != null)
               {
                   using (var stream = entry.Open())
                   {
                       BitmapImage bitmap = new BitmapImage();
                       bitmap.BeginInit();
                       bitmap.StreamSource = stream;
                       bitmap.CacheOption = BitmapCacheOption.OnLoad; // Pour éviter les problèmes de flux
                       bitmap.EndInit();
                       bitmap.Freeze(); // Pour rendre l'image thread-safe

                       // Afficher l'image dans un contrôle Image
                       Image imageControl = new Image();
                       imageControl.Source = bitmap;
                       this.Content = imageControl; // Ajoute l'image à la fenêtre principale
                   }
               }
           }
       }
   }
}
 */