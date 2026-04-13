namespace Common.Discovery;

public interface IFindFiles
{
    ISet<FoundFilesDto> FindFilesInFolders(List<string> roots);
    ISet<FoundFilesDto> FindFilesInFoldersAndSubFolders(List<string> roots);
}