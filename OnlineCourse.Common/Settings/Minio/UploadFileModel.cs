namespace OnlineCourse.Common.Settings;

public record UploadFileModel(string FileName, string ContentType, long Size, Stream Data);