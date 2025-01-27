using System;
using System.Collections.Generic;
using UiDesigner.Application.Dto.UiDto;

namespace UiDesigner.Application.Dto.JsonDto;

public class ProjectJsonDto
{
    public string ZipPath { get; init; }
    public string Id { get; init; }
    public string UniqueId { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public List<ReportInfoUiDto> ReportInfos { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}