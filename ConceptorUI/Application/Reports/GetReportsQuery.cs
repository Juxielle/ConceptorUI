using System.Collections.Generic;
using ConceptorUI.Application.Dto.UiDto;

namespace ConceptorUI.Application.Reports;

public class GetReportsQuery
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public List<ReportInfoUiDto> ReportInfos { get; init; }
}