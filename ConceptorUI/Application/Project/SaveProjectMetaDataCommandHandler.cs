using System;
using System.IO;
using System.Threading.Tasks;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Project;

public class SaveProjectMetaDataCommandHandler
{
    public async Task<Result<bool>> Handle(SaveProjectMetaDataCommand command)
    {
        try
        {
            const string streamName = "metadata";

            await using var fs = new FileStream(command.ZipPath + ":" + streamName, FileMode.Create);
            await using var writer = new StreamWriter(fs);
            
            await writer.WriteLineAsync("{");
            await writer.WriteLineAsync($@"{nameof(command.ZipPath)}: {command.ZipPath}");
            await writer.WriteLineAsync($@"{nameof(command.Name)}: {command.Name}");
            await writer.WriteLineAsync($@"{nameof(command.Id)}: {command.Id}");
            await writer.WriteLineAsync($@"{nameof(command.Image)}: {command.Image}");
            await writer.WriteLineAsync($@"{nameof(command.Created)}: {command.Created}");
            await writer.WriteLineAsync($@"{nameof(command.Updated)}: {command.Updated}");
            await writer.WriteLineAsync("}");
            
            return Result<bool>.Success(true);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(Error.NotFound);
        }
    }
}