using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UiDesigner.Application.Dto.JsonDto;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.PlatformSystem;

public class GetProjectInfoQueryHandler
{
    public async Task<Result<IEnumerable<ProjectInfoUiDto>>> Handle(GetProjectInfoQuery request)
    {
        try
        {
            var jsonsDto = JsonSerializer.Deserialize<List<ProjectInfoJsonDto>>(
                await File.ReadAllTextAsync(request.SystemPath)
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
            }).ToList();
            
            return Result<IEnumerable<ProjectInfoUiDto>>.Success(uisDto);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<ProjectInfoUiDto>>.Failure(Error.NotFound);
        }
    }
}