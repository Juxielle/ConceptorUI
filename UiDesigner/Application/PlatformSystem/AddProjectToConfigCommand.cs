using UiDesigner.Application.Project;

namespace UiDesigner.Application.PlatformSystem;

public class AddProjectToConfigCommand
{
    public string SystemPath { get; init; }
    public SaveProjectMetaDataCommand ProjectCommand { get; init; }
}