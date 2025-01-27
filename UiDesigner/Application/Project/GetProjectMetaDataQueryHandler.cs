using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using UiDesigner.Application.Dto.JsonDto;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Project;

public class GetProjectMetaDataQueryHandler
{
    public async Task<Result<ProjectInfoUiDto>> Handle(GetProjectMetaDataQuery request)
    {
        try
        {
            const string streamName = "metadata";

            await using var fs = new FileStream(request.ZipPath + ":" + streamName, FileMode.Open);
            using var reader = new StreamReader(fs);

            var buffer = new char[1024];
            int bytesRead;
            var json = string.Empty;
            
            while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                try
                {
                    json += new string(buffer, 0, bytesRead);
                }
                catch (Exception e)
                {
                    Console.WriteLine($@"Error Message: {e.Message}");
                }
            }

            json = json.Replace(@"\", @"\\");
            
            var jsonDto = JsonSerializer.Deserialize<ProjectInfoJsonDto>(json);
            if (jsonDto == null) throw new Exception();

            return Result<ProjectInfoUiDto>.Success(new ProjectInfoUiDto
            {
                ZipPath = jsonDto.ZipPath,
                Id = jsonDto.Id,
                UniqueId = jsonDto.UniqueId,
                Name = jsonDto.Name,
                Image = jsonDto.Image,
                Created = jsonDto.Created,
                Updated = jsonDto.Updated
            });
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
            return Result<ProjectInfoUiDto>.Failure(Error.NotFound);
        }
    }
}