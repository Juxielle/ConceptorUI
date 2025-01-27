using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Project;

public class SaveProjectCommandHandler
{
    public Task<Result<bool>> Handle(SaveProjectCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            foreach (var report in command.Reports)
            {
                var entry = archive.GetEntry($@"{command.ProjectName}/Pages/{report.Name}.json");
                if (entry != null)
                {
                    entry.Delete();
                    var updatedEntry = archive.CreateEntry($@"{command.ProjectName}/Pages/{report.Name}.json");
                    using var writer = new StreamWriter(updatedEntry.Open());
                    
                    writer.WriteLine(report.Json);
                    Console.WriteLine($"Modification du fichier: {command.ProjectName}/Pages/{report.Name}.json");
                }
                else
                {
                    Console.WriteLine($"Le fichier {report.Name} - {command.ProjectName} n'a pas été trouvé dans l'archive.");
                }
            }
            
            return Task.FromResult(Result<bool>.Success(true));
        }
        catch (Exception)
        {
            return Task.FromResult(Result<bool>.Failure(Error.NotFound));
        }
    }
}