using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace ConceptorUI.Application.Project;

public class SaveProjectCommandHandler
{
    public async Task Handle(SaveProjectCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            foreach (var report in command.Reports)
            {
                var entry = archive.GetEntry($@"{command.ProjectName}/Pages/{report.Name}.json");
                if (entry != null)
                {
                    await using var writer = new StreamWriter(entry.Open());
                    await writer.WriteLineAsync(report.Json);
                    Console.WriteLine($"Modification du fichier: {command.ProjectName}/Pages/{report.Name}.json");
                }
                else
                {
                    Console.WriteLine($"Le fichier {report.Name} - {command.ProjectName} n'a pas été trouvé dans l'archive.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
        }
    }
}