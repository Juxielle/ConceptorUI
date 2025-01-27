using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Configs;

public class SaveConfigCommandHandler
{
    public async Task<Result<bool>> Handle(SaveConfigCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);

            var entry = archive.GetEntry($"{command.ProjectName}/config.json");
            if (entry != null)
            {
                entry.Delete();
                
                var updatedEntry = archive.CreateEntry($"{command.ProjectName}/config.json");
                await using var writer = new StreamWriter(updatedEntry.Open());
                await writer.WriteLineAsync(command.Json);

                return Result<bool>.Success(true);
            }

            throw new Exception();
        }
        catch (Exception)
        {
            return Result<bool>.Failure(Error.NotFound);
        }
    }
}