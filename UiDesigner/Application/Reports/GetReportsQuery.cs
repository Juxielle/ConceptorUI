using System.Collections.Generic;
using UiDesigner.Application.Dto.UiDto;

namespace UiDesigner.Application.Reports;

public class GetReportsQuery
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public List<ReportInfoUiDto> ReportInfos { get; init; }
}