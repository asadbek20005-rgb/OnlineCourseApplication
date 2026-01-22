using Microsoft.AspNetCore.Http;
using StatusGeneric;

namespace OnlineCourse.Service.Helpers;

public interface IContentService : IStatusGeneric
{
    Task<int?> CreateContentForImage(IFormFile? file, string folderName);
    Task<int?> UpdateContentForImage(int id, IFormFile file);
    Task<int?> CreateContentForVideo(IFormFile? file, string folderName);
    Task<int?> UpdateContentForVideo(int id, IFormFile file);
    Task<(Stream, string)> GetContentForVideo(int id, string folderName,string url);
    Task<(Stream, string)> DownloadFileAsync(string url);
}
