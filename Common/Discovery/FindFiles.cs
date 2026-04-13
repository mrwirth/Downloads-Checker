namespace Common.Discovery;

public class FindFiles : IFindFiles
{
    public ISet<FoundFilesDto> FindFilesInFolders(List<string> roots)
    {
        throw new NotImplementedException();
    }

    public ISet<FoundFilesDto> FindFilesInFoldersAndSubFolders(List<string> roots)
    {
        throw new NotImplementedException();
    }

    private (HashSet<FoundFileDto> files, string? error) GetFiles(string path)
    {
        HashSet<FoundFileDto> files = [];
        string? error = null;

        try
        {
            files = new HashSet<FoundFileDto>(Directory.EnumerateFiles(path).Select(f => new FoundFileDto(f)));
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (files, error);
    }

    private (HashSet<string> files, string? error) GetDirectories(string path)
    {
        HashSet<string> directories = [];
        string? error = null;

        try
        {
            directories = new HashSet<string>(Directory.EnumerateDirectories(path));
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (directories, error);
    }

    private FoundFilesDto FindFromPaths(IEnumerable<string> paths, bool recursive)
    {
        HashSet<FoundFileDto> files = [];
        HashSet<FindFilesErrorDto> errors = [];

        HashSet<string> visited = [];
        var toVisit = new Stack<string>(paths);

        while (toVisit.Any())
        {
            var path = toVisit.Pop();

            if (visited.Contains(path))
            {
                // Check if path has already been visit.  If so, log and skip.
            }

            var result = GetFiles(path);
            if (result.error is not null)
                errors.Add(new FindFilesErrorDto(path, result.error));
            else
                files.UnionWith(result.files);
        }

        return new FoundFilesDto(files, errors);
    }
}