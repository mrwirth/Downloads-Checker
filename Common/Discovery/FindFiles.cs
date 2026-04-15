using Common.Services;

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


    private static FoundFilesDto FindFromPaths(IEnumerable<string> paths, bool recursive, IFileService fileService)
    {
        HashSet<FileDto> files = [];
        HashSet<DirectoryEnumerationErrorDto> errors = [];

        HashSet<string> visited = [];
        var toVisit = new Stack<string>(paths);

        while (toVisit.Count != 0)
        {
            var path = toVisit.Pop();

            var haveVisited = !visited.Add(path);
            if (haveVisited)
            {
                errors.Add(new DirectoryEnumerationErrorDto(path, "Already visited this path."));
                continue;
            }

            var newFiles = fileService.EnumerateFiles(path);
            if (newFiles.Error is not null)
                errors.Add(new DirectoryEnumerationErrorDto(path, newFiles.Error.Message));
            else
                files.UnionWith(newFiles.FileDtos);

            if (recursive)
            {
                var newDirectories = fileService.EnumerateDirectories(path);
                if (newDirectories.Error is not null)
                    errors.Add(new DirectoryEnumerationErrorDto(path, newDirectories.Error.Message));
                else
                    foreach (var directory in newDirectories.DirectoryPaths)
                        toVisit.Push(directory);
            }
        }

        return new FoundFilesDto(files, errors);
    }
}