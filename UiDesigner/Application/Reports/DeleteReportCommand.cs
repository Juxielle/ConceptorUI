namespace UiDesigner.Application.Reports;

public class DeleteReportCommand
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public string FileName { get; init; }
    public string? FileCode { get; init; }
}