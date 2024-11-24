using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace ConceptorUI.Application.Project;

public class SaveConfigCommandHandler
{
    public async Task Handle(SaveConfigCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            
            var entry = archive.GetEntry($@"{command.ProjectName}/config.json");
            if (entry != null)
            {
                await using var writer = new StreamWriter(entry.Open());
                await writer.WriteLineAsync(command.Json);
            }
            else throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error saving config: {e.Message}");
        }
    }
}