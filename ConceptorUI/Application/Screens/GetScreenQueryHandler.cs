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

public class GetScreenQueryHandler
{
    public async Task<Result<ScreenUiDto>> Handle(GetScreenQuery request)
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
                ScreenName = x.ScreenName,
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

            var uiDto = uisDto.Find(x => x.ScreenName == request.ScreenName);
            
            if (uiDto == null) throw new Exception();

            return Result<ScreenUiDto>.Success(uiDto);
        }
        catch (Exception)
        {
            return Result<ScreenUiDto>.Failure(Error.NotFound);
        }
    }
}