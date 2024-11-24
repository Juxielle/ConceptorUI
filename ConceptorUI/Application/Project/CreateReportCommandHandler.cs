using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace ConceptorUI.Application.Project;

public class CreateReportCommandHandler
{
    public async Task Handle(CreateReportCommand command)
    {
        try
        {
            await using var zipToOpen = new FileStream(command.ZipPath, FileMode.Open);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            var entry = archive.CreateEntry($@"{command.ProjectName}/Pages/{command.Report.Name}.json");

            await using var writer = new StreamWriter(entry.Open());
            await writer.WriteLineAsync(command.Report.Json);
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error creating report: {e.Message}");
        }
    }
}