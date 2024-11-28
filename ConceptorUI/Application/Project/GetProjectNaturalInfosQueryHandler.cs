using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Domain.ValueObjects;

namespace ConceptorUI.Application.Project;

public class GetProjectNaturalInfosQueryHandler
{
    public Task<Result<ProjectNaturalInfosUiDto>> Handle(GetProjectNaturalInfosQuery request)
    {
        try
        {
            if (!Directory.Exists(request.ZipPath))
                throw new Exception();
            
            string fileName;
            var image = string.Empty;

            using (var archive = ZipFile.OpenRead(request.ZipPath))
            {
                if (archive.Entries.Count != 1)
                    throw new Exception();
                
                fileName = archive.Entries[0].FullName.Replace($"{request.ZipPath}/", "");
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