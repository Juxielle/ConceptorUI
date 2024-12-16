using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Reports;

public class CreateReportCommandHandler
{
    public async Task<Result<bool>> Handle(CreateReportCommand command)
    {
        try
        {
            await using var zipToOpen = new FileStream(command.ZipPath, FileMode.Open);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            var entry = archive.CreateEntry($@"{command.ProjectName}/Pages/{command.Report.Name}.json");

            await using var writer = new StreamWriter(entry.Open());
            await writer.WriteLineAsync(command.Report.Json);
            
            return Result<bool>.Success(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(@$"Echec d'enregistrement: {e.Message}");
            return Result<bool>.Failure(Error.NotFound);
        }
    }
}