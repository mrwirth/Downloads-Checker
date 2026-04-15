using Common.Services;

namespace Common.Discovery;

public record DirectoryEnumerationErrorDto(string DirectoryPath, string ErrorMessage);

public record FoundFilesDto(IEnumerable<FileDto> Files, IEnumerable<DirectoryEnumerationErrorDto> Errors);