﻿using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Project;

public class CreateProjectCommandHandler
{
    public async Task<Result<ProjectInfoUiDto>> Handle(CreateProjectCommand command)
    {
        try
        {
            var projectPath = Path.Combine(command.FolderPath, $"{command.ProjectName}_extern");
            var projectPathIntern = Path.Combine(projectPath, command.ProjectName);
            var filePath = Path.Combine(command.FolderPath, $"{command.ProjectName}.uix");
            
            Directory.CreateDirectory(projectPath);
            Directory.CreateDirectory(projectPathIntern);
            Directory.CreateDirectory($@"{projectPathIntern}\Pages");
            Directory.CreateDirectory($@"{projectPathIntern}\Components");
            Directory.CreateDirectory($@"{projectPathIntern}\Medias");
            Directory.CreateDirectory($@"{projectPathIntern}\Caches");
            Directory.CreateDirectory($@"{projectPathIntern}\Datas");
            
            await File.Create($@"{projectPathIntern}\config.json").DisposeAsync();
            
            ZipFile.CreateFromDirectory(projectPath, filePath);
            Directory.Delete(projectPath, true);
            
            return Result<ProjectInfoUiDto>.Success(new ProjectInfoUiDto
            {
                Id = command.ProjectId,
                Name = command.ProjectName,
                ZipPath = filePath,
                Image = command.ProjectImage,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
        }
        catch (Exception)
        {
            return Result<ProjectInfoUiDto>.Failure(Error.NotFound);
        }
    }
}