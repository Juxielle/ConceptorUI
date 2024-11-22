using System;
using System.IO.Compression;

namespace ConceptorUI.Application.Project;

public class DeleteImageCommandHandler
{
    public void Handle(DeleteImageCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            var entry = archive.GetEntry($@"{command.ProjectName}/Medias/{command.FileName}");
            entry?.Delete();
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
        }
    }
}