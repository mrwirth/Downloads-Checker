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

    private (HashSet<string> directories, string? error) GetDirectories(string path)
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

        while (toVisit.Count != 0)
        {
            var path = toVisit.Pop();

            var haveVisited = !visited.Add(path);
            if (haveVisited)
            {
                errors.Add(new FindFilesErrorDto(path, "Already visited this path."));
                continue;
            }

            var newFiles = GetFiles(path);
            if (newFiles.error is not null)
                errors.Add(new FindFilesErrorDto(path, newFiles.error));
            else
                files.UnionWith(newFiles.files);

            if (recursive)
            {
                var newDirectories = GetDirectories(path);
                if (newDirectories.error is not null)
                    errors.Add(new FindFilesErrorDto(path, newDirectories.error));
                else
                    foreach (var directory in newDirectories.directories)
                        toVisit.Push(directory);
            }
        }

        return new FoundFilesDto(files, errors);
    }
}