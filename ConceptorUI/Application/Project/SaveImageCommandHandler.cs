using System;
using System.IO.Compression;

namespace ConceptorUI.Application.Project;

public class SaveImageCommandHandler
{
    public void Handle(SaveImageCommand command)
    {
        try
        {
            using var archive = ZipFile.Open(command.ZipPath, ZipArchiveMode.Update);
            archive.CreateEntryFromFile(command.FilePath, command.FileName);
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error: {e.Message}");
        }
    }
}