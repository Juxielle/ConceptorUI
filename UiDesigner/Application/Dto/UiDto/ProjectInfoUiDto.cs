using System;

namespace UiDesigner.Application.Dto.UiDto;

public class ProjectInfoUiDto
{
    public string ZipPath { get; init; }
    public string Id { get; init; }
    public string UniqueId { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}