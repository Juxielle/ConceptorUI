using System.Collections.Generic;
using UiDesigner.Domain.Entities;

namespace UiDesigner.Application.Project;

public class SaveProjectCommand
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public List<Domain.Entities.Report> Reports { get; init; }
}