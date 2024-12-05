using ConceptorUI.Application.Project;

namespace ConceptorUI.Application.PlatformSystem;

public class AddProjectToConfigCommand
{
    public string SystemPath { get; init; }
    public SaveProjectMetaDataCommand ProjectCommand { get; init; }
}