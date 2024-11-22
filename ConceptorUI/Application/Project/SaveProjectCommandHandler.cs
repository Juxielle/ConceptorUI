using System;
using System.IO;
using System.IO.Compression;

namespace ConceptorUI.Application.Project;

public class SaveProjectCommandHandler
{
    public void Handle(SaveProjectCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            foreach (var report in command.Reports)
            {
                var entry = archive.GetEntry($@"{command.ProjectName}/Pages/{report.Name}.json");
                if (entry != null)
                {
                    using var writer = new StreamWriter(entry.Open());
                    writer.WriteLine(report.Json);
                    Console.WriteLine($"Modification du fichier: {report.Name}");
                }
                else
                {
                    Console.WriteLine($"Le fichier {report.Name} n'a pas été trouvé dans l'archive.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
        }
    }
}