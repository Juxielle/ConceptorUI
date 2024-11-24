using System;
using System.Collections.Generic;

namespace ConceptorUI.Application.Dto.UiDto;

public class ProjectUiDto
{
    public string ZipPath { get; init; }
    public string Id { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public List<ReportInfoUiDto> ReportInfos { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}