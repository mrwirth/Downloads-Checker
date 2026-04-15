namespace Common.Services;

public class FileService : IFileService
{
    public FileEnumerationResult EnumerateFiles(string directoryPath)
    {
        HashSet<FileDto> files = [];
        Exception? error = null;

        try
        {
            files = new HashSet<FileDto>(Directory.EnumerateFiles(directoryPath).Select(f => new FileDto(f)));
        }
        catch (Exception e)
        {
            error = e;
        }

        return new FileEnumerationResult(files, error);
    }

    public DirectoryEnumerationResult EnumerateDirectories(string directoryPath)
    {
        HashSet<string> directories = [];
        Exception? error = null;

        try
        {
            directories = new HashSet<string>(Directory.EnumerateDirectories(directoryPath));
        }
        catch (Exception e)
        {
            error = e;
        }

        return new DirectoryEnumerationResult(directories, error);
    }
}