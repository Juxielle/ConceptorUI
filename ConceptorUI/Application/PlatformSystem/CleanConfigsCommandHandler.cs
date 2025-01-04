using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto.JsonDto;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.PlatformSystem;

public class CleanConfigsCommandHandler
{
    public async Task<Result<bool>> Handle(CleanConfigsCommand command)
    {
        try
        {
            if (!Directory.Exists(command.SystemPath))
                throw new Exception();
            
            var configFolderPath = Path.Combine(command.SystemPath, "Configs");
            var filePath = Path.Combine(configFolderPath, "config.json");
            
            var jsonsDto = JsonSerializer.Deserialize<List<ProjectInfoJsonDto>>(
                await File.ReadAllTextAsync(filePath)
            );

            if (jsonsDto == null)
                throw new Exception();

            var uisDto = jsonsDto.Select(p => new ProjectInfoUiDto
            {
                ZipPath = p.ZipPath,
                Id = p.Id,
                UniqueId = p.UniqueId,
                Name = p.Name,
                Image = p.Image,
                Created = p.Created,
                Updated = p.Updated
            }).Where(p => File.Exists(p.ZipPath)).ToList();

            var uisDtoWithoutDuplicates = new List<ProjectInfoUiDto>();
            foreach (var item in uisDto)
            {
                var found = false;
                foreach (var item2 in uisDtoWithoutDuplicates)
                {
                    if(item.ZipPath != item2.ZipPath || item.Id != item2.Id) continue;
                    found = true;
                }
                if(found) continue;
                uisDtoWithoutDuplicates.Add(item);
            }

            var json = JsonSerializer.Serialize(uisDtoWithoutDuplicates);
            var saveResult = await new SaveSystemConfigCommandHandler().Handle(new SaveSystemConfigCommand
            {
                Path = command.SystemPath,
                Content = json
            });

            if (saveResult.IsFailure)
                throw new Exception();

            return Result<bool>.Success(true);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(Error.NotFound);
        }
    }
}