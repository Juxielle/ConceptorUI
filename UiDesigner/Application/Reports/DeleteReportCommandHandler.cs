using System;
using System.IO.Compression;
using System.Threading.Tasks;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Reports;

public class DeleteReportCommandHandler
{
    public Task<Result<bool>> Handle(DeleteReportCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            var entry = archive.GetEntry($@"{command.ProjectName}/Pages/{command.FileCode}.json");
            entry?.Delete();

            return Task.FromResult(Result<bool>.Success(true));
        }
        catch (Exception)
        {
            return Task.FromResult(Result<bool>.Failure(Error.NotFound));
        }
    }
}