using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto.JsonDto;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Constants;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Project;

public class GetProjectInfoQueryHandler
{
    public async Task<Result<IEnumerable<ProjectInfoUiDto>>> Handle(GetProjectInfoQuery request)
    {
        try
        {
            var jsonsDto = JsonSerializer.Deserialize<List<ProjectInfoJsonDto>>(
                await File.ReadAllTextAsync(Env.FileConfig)
            );
            
            if (jsonsDto == null)
                throw new Exception();
            
            var uisDto = jsonsDto.Select(p => new ProjectInfoUiDto
            {
                ZipPath = p.ZipPath,
                Id = p.Id,
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