using System;
using System.IO;
using System.Threading.Tasks;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Project;

public class SaveProjectMetaDataCommandHandler
{
    public async Task<Result<bool>> Handle(SaveProjectMetaDataCommand command)
    {
        try
        {
            const string streamName = "metadata";

            await using var fs = new FileStream(command.ZipPath + ":" + streamName, FileMode.Create);
            await using var writer = new StreamWriter(fs);

            var createdDate =
                $"{command.Created.Year}-{FormatNumber(command.Created.Month)}-{FormatNumber(command.Created.Day)}";
            var updatedDate =
                $"{command.Updated.Year}-{FormatNumber(command.Created.Month)}-{FormatNumber(command.Created.Day)}";

            await writer.WriteLineAsync("{");
            await writer.WriteLineAsync($"\"{nameof(command.ZipPath)}\": \"{command.ZipPath}\",");
            await writer.WriteLineAsync($"\"{nameof(command.Name)}\": \"{command.Name}\",");
            await writer.WriteLineAsync($"\"{nameof(command.Id)}\": \"{command.Id}\",");
            await writer.WriteLineAsync($"\"{nameof(command.UniqueId)}\": \"{command.UniqueId}\",");
            await writer.WriteLineAsync($"\"{nameof(command.Image)}\": \"{command.Image}\",");
            await writer.WriteLineAsync($"\"{nameof(command.Created)}\": \"{createdDate}\",");
            await writer.WriteLineAsync($"\"{nameof(command.Updated)}\": \"{updatedDate}\"");
            await writer.WriteLineAsync("}");

            return Result<bool>.Success(true);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(Error.NotFound);
        }
    }

    private static string FormatNumber(int value) => value <= 9 ? $"0{value}" : value.ToString();
}