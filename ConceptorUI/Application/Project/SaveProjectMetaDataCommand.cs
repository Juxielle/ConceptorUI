using System;

namespace ConceptorUI.Application.Project;

public class SaveProjectMetaDataCommand
{
    public string ZipPath { get; init; }
    public string Id { get; init; }
    public string UniqueId { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}