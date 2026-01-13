using OnlineCourse.Common.Settings;
using StatusGeneric;

namespace OnlineCourse.Service.Infrastructure;

public interface IMinioService : IStatusGeneric
{
    Task UploadFileAsync(string folderName, UploadFileModel file);
    Task<UploadFileModel?> GetFileAsync(string folderName, string fileName);
    Task RemoveFileAsync(string folderName, string fileName);
    Task<(Stream, string)> DownloadFileAsync(string folderName, string fileName);
}
