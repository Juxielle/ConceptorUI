﻿namespace UiDesigner.Application.Images;

public class SaveImageCommand
{
    public string ZipPath { get; init; }
    public string ProjectName { get; init; }
    public string FilePath { get; init; }
    public string FileName { get; init; }
}