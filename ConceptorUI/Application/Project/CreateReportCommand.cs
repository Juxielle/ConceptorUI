using ConceptorUI.Domain.Entities;

namespace ConceptorUI.Application.Project;

public class CreateReportCommand
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public Report Report { get; init; }
}