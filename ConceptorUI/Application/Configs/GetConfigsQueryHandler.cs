using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto.JsonDto;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Configs;

public class GetConfigsQueryHandler
{
    public async Task<Result<ProjectUiDto>> Handle(GetConfigsQuery request)
    {
        try
        {
            await using var fs = File.OpenRead(request.ZipPath);
            using var fileArchive = ZipFile.Open(request.ZipPath, ZipArchiveMode.Read);
            
            var entry = fileArchive.GetEntry($@"{request.ProjectName.Replace(@"\", "")}/config.json");
            if (entry == null) throw new Exception();
            
            using var reader = new StreamReader(entry.Open());
            
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

            var jsonDto = System.Text.Json.JsonSerializer.Deserialize<ProjectJsonDto>(json);
            if (jsonDto == null) throw new Exception();
            
            return Result<ProjectUiDto>.Success(new ProjectUiDto
            {
                Id = jsonDto.Id,
                UniqueId = jsonDto.UniqueId,
                Name = jsonDto.Name,
                Image = jsonDto.Image,
                ZipPath = jsonDto.ZipPath,
                Created = jsonDto.Created,
                Updated = jsonDto.Updated,
                ReportInfos = jsonDto.ReportInfos
            });
        }
        catch (Exception)
        {
            return Result<ProjectUiDto>.Failure(Error.NotFound);
        }
    }
}