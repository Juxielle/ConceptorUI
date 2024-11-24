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
            var files = new List<string>();

            await using (var zipToOpen = new FileStream(request.ZipPath, FileMode.Open))
            using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                foreach (var folderEntry in archive.Entries)
                {
                    if (folderEntry.FullName.StartsWith($"{request.ProjectName.Replace(@"\", "")}/Pages/"))
                    {
                        var fileName = Path.GetFileName(folderEntry.FullName);
                        if(fileName is null or "") continue;
                        
                        files.Add(fileName);
                    }
                }
            }

            await using (var fs = File.OpenRead(request.ZipPath))
            using (var fileArchive = ZipFile.Open(request.ZipPath, ZipArchiveMode.Read))
            {
                foreach (var file in files)
                {
                    var entry = fileArchive.GetEntry($@"{request.ProjectName.Replace(@"\", "")}/Pages/{file}");
                    if (entry != null)
                    {
                        using var reader = new StreamReader(entry.Open());
                        
                        var buffer = new char[1024];
                        int bytesRead;
                        var json = string.Empty;
                        
                        while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            try
                            {
                                json += new string(buffer, 0, bytesRead);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($@"Error Message: {e.Message}");
                            }
                        }
                        
                        reports.Add(new ReportUiDto
                        {
                            Name = file,
                            Code = file,
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

            return reports.AsEnumerable();
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
            return new List<ReportUiDto>().AsEnumerable();
        }
    }
}