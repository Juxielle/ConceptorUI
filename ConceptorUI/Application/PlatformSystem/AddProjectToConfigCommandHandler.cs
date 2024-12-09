﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto.JsonDto;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.PlatformSystem;

public class AddProjectToConfigCommandHandler
{
    public async Task<Result<bool>> Handle(AddProjectToConfigCommand command)
    {
        try
        {
            if (!Directory.Exists(command.SystemPath))
                throw new Exception();
            var configFolderPath = Path.Combine(command.SystemPath, "Configs");
            var filePath = Path.Combine(configFolderPath, "config.json");

            if (!Directory.Exists(configFolderPath))
                Directory.CreateDirectory(configFolderPath);
            if (!File.Exists(filePath))
                File.Create(filePath);

            var jsonsDto = JsonSerializer.Deserialize<List<ProjectInfoJsonDto>>(
                await File.ReadAllTextAsync(filePath)
            ) ?? [];

            jsonsDto.Add(new ProjectInfoJsonDto
            {
                ZipPath = command.ProjectCommand.ZipPath,
                Id = command.ProjectCommand.Id,
                Name = command.ProjectCommand.Name,
                Image = command.ProjectCommand.Image,
                Created = command.ProjectCommand.Created,
                Updated = command.ProjectCommand.Updated
            });

            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(jsonsDto));

            return Result<bool>.Success(true);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(Error.NotFound);
        }
    }
}