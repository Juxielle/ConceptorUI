using System;
using System.IO.Compression;
using System.Threading.Tasks;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Images;

public class DeleteImageCommandHandler
{
    public Task<Result<bool>> Handle(DeleteImageCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            var entry = archive.GetEntry($@"{command.ProjectName}/Medias/{command.FileName}");
            entry?.Delete();
            
            return Task.FromResult(Result<bool>.Success(true));
        }
        catch (Exception)
        {
            return Task.FromResult(Result<bool>.Failure(Error.NotFound));
        }
    }
}