using System;

namespace ConceptorUI.Application.Dto.JsonDto;

public class ProjectInfoJsonDto
{
    public string ZipPath { get; init; }
    public string Id { get; init; }
    public string UniqueId { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}