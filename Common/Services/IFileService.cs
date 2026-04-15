namespace Common.Services;

public record FileDto(string Path);

public record DirectoryEnumerationResult(HashSet<string> DirectoryPaths, Exception? Error = null);

public record FileEnumerationResult(HashSet<FileDto> FileDtos, Exception? Error = null);

public interface IFileService
{
    DirectoryEnumerationResult EnumerateDirectories(string directoryPath);
    FileEnumerationResult EnumerateFiles(string directoryPath);
}