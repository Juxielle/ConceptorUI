using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ConceptorUI.Application.Dto;

namespace ConceptorUI.Application.Project;

public class GetReportsQueryHandler
{
    public async Task<IEnumerable<ReportUiDto>> Handle(GetReportsQuery request)
    {
        try
        {
            var reports = new List<ReportUiDto>();

            await using (var zipToOpen = new FileStream(request.ZipPath, FileMode.Open))
            using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                foreach (var folderEntry in archive.Entries)
                {
                    if (folderEntry.FullName.StartsWith($"{request.ProjectName.Replace(@"\", "")}/Pages/"))
                    {
                        var fileName = Path.GetFileName(folderEntry.FullName);

                        Console.WriteLine($@"{request.ProjectName.Replace(@"\", "")}\{fileName}");
                        var entry = archive.GetEntry($@"{request.ProjectName.Replace(@"\", "")}\{fileName}");
                        if (entry != null)
                        {
                            using var reader = new StreamReader(entry.Open());
                            var json = await reader.ReadToEndAsync();

                            reports.Add(new ReportUiDto
                            {
                                Name = fileName,
                                Code = fileName,
                                Date = DateTime.Now,
                                Json = json
                            });
                        }
                        else
                        {
                            Console.WriteLine($"Le fichier n'a pas été trouvé dans l'archive.");
                        }
                    }
                }
            }

            return reports.AsEnumerable();
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
            return new List<ReportUiDto>().AsEnumerable();
        }
    }
}