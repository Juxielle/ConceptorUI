using System;
using System.IO;
using System.Threading.Tasks;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.PlatformSystem;

public class SaveSystemConfigCommandHandler
{
    public async Task<Result<bool>> Handle(SaveSystemConfigCommand command)
    {
        try
        {
            if (!Directory.Exists(command.Path))
                throw new Exception();
            var configFolderPath = Path.Combine(command.Path, "Configs");
            var filePath = Path.Combine(configFolderPath, "config.json");

            if (!Directory.Exists(configFolderPath))
                Directory.CreateDirectory(configFolderPath);
            if (!File.Exists(filePath))
                File.Create(filePath);
            
            await File.WriteAllTextAsync(filePath, command.Content);
            
            return Result<bool>.Success(true);
        }
        catch (Exception)
        {
            Console.WriteLine($@"Echec d'Enregistrement des configs.");
            return Result<bool>.Failure(Error.NotFound);
        }
    }
}