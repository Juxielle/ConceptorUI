using System.Collections.Generic;
using ConceptorUI.Domain.Entities;

namespace ConceptorUI.Application.Project;

public class SaveProjectCommand
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public List<Report> Reports { get; init; }
}