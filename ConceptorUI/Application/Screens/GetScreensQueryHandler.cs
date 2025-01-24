using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto.JsonDto;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Screens;

public class GetScreensQueryHandler
{
    public async Task<Result<IEnumerable<ScreenUiDto>>> Handle(GetScreensQuery request)
    {
        try
        {
            var jsonsDto = JsonSerializer.Deserialize<List<ScreenJsonDto>>(
                await File.ReadAllTextAsync(request.Path)
            );

            if (jsonsDto == null || jsonsDto.Count == 0)
                throw new Exception();

            var uisDto = jsonsDto.Select(x => new ScreenUiDto
            {
                Label = x.Label,
                PlatformName = x.PlatformName,
                Width = x.Width,
                Ratio = x.Ratio,
                MarginLeft = x.MarginLeft,
                MarginRight = x.MarginRight,
                MarginTop = x.MarginTop,
                MarginBottom = x.MarginBottom,
                BorderTopLeftRadius = x.BorderTopLeftRadius,
                BorderTopRightRadius = x.BorderTopRightRadius,
                BorderBottomLeftRadius = x.BorderBottomLeftRadius,
                BorderBottomRightRadius = x.BorderBottomRightRadius
            }).ToList();

            return Result<IEnumerable<ScreenUiDto>>.Success(uisDto);
        }
        catch (Exception)
        {
            return Result<IEnumerable<ScreenUiDto>>.Failure(Error.NotFound);
        }
    }
}