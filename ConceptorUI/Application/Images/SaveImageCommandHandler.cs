using System;
using System.IO.Compression;
using System.Threading.Tasks;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Images;

public class SaveImageCommandHandler
{
    public Task<Result<bool>> Handle(SaveImageCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            archive.CreateEntryFromFile(command.FilePath, $"{command.ProjectName}/{command.FileName}");
            
            return Task.FromResult(Result<bool>.Success(true));
        }
        catch (Exception e)
        {
            return Task.FromResult(Result<bool>.Failure(Error.NotFound));
        }
    }
}