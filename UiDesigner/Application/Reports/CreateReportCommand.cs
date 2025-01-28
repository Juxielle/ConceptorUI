﻿namespace UiDesigner.Application.Reports;

public class CreateReportCommand
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public Domain.Entities.Report Report { get; init; }
}