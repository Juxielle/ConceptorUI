using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Domain.ValueObjects;

namespace UiDesigner.Application.Project;

public class GetProjectNaturalInfosQueryHandler
{
    public Task<Result<ProjectNaturalInfosUiDto>> Handle(GetProjectNaturalInfosQuery request)
    {
        try
        {
            if (!File.Exists(request.ZipPath))
                throw new Exception();
            
            var fileName = string.Empty;
            var image = string.Empty;

            using (var archive = ZipFile.OpenRead(request.ZipPath))
            {
                if (archive.Entries.Count == 0)
                    throw new Exception();
                
                var path = archive.Entries[0].FullName.Replace($"{request.ZipPath}/", "");
                fileName = path.TakeWhile(c => c != '/').Aggregate(fileName, (current, c) => current + c);

                foreach (var entry in archive.Entries)
                {
                    if (!entry.FullName.StartsWith($"{fileName}/app")) continue;
                    image = entry.FullName.Replace($"{request.ZipPath}/{fileName}/", "");
                }
            }
            
            return Task.FromResult(Result<ProjectNaturalInfosUiDto>.Success(new ProjectNaturalInfosUiDto
            {
                OriginalName = fileName,
                Image = image
            }));
        }
        catch (Exception)
        {
            return Task.FromResult(Result<ProjectNaturalInfosUiDto>.Failure(Error.NotFound));
        }
    }
}