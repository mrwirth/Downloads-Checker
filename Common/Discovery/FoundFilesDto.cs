namespace Common.Discovery;

public record FoundFileDto(string Path);

public record FindFilesErrorDto(string Directory, string ErrorMessage);

public record FoundFilesDto(IEnumerable<FoundFileDto> Files, IEnumerable<FindFilesErrorDto> Errors);