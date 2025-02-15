﻿using System;

namespace UiDesigner.Application.Dto.UiDto;

public class ReportUiDto
{
    public string Name { get; init; }
    public string? Code { get; init; }
    public DateTime Date { get; init; }
    public string Json { get; init; }
}