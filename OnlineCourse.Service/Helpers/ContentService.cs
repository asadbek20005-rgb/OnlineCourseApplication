using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Settings;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Infrastructure;
using StatusGeneric;

namespace OnlineCourse.Service.Helpers;
using static OnlineCourse.Common.Constants.ContentTypeConstant;
public class ContentService(IMinioService minioService, IUnitOfWork unitOfWork) : StatusGenericHandler, IContentService
{

    private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png" };
    private readonly string[] _permittedMimeTypes = { "image/jpeg", "image/png" };
    private readonly string[] _allowedVideoTypes = { Mp4, Avi, Mkv, Mov, Wmv };

    public async Task<int?> CreateContentForImage(IFormFile? file, string folderName)
    {
        bool isValidFile = ValidateFile(file);
        if (!isValidFile) return null;

        var result = await ProcessFileAsync(file!, folderName);
        if (result is null) return null;

        var (uploadModel, contentTypeId) = result.Value;

        var content = new Content
        {
            Name = file!.Name,
            Url = uploadModel.FileName,
            ContentTypeId = contentTypeId,
            FolderName = folderName,
            CreatedAt = DateTime.UtcNow,
        };

        await unitOfWork.ContentRepository().AddAsync(content);
        await unitOfWork.SaveChangesAsync();
        return content.Id;
    }

    public async Task<int?> CreateContentForVideo(IFormFile? file, string folderName)
    {
        bool isValidFile = ValdateFileForVideo(file);
        if (!isValidFile) return null;

        var uploadModel = await PrepareAndUploadFileAsync(file!, folderName);
        if (uploadModel is null) return null;
        int contentTypeId = await GetContentTypeIdAsync(uploadModel);

        var content = new Content
        {
            Name = file!.Name,
            Url = uploadModel.FileName,
            ContentTypeId = contentTypeId,
            FolderName = folderName
        };

        await unitOfWork.ContentRepository().AddAsync(content);
        await unitOfWork.SaveChangesAsync();
        return content.Id;
    }

    public async Task<int?> UpdateContentForImage(int id, IFormFile file)
    {
        try
        {
            bool isValidFile = ValidateFile(file);
            if (!isValidFile) return null;
            var content = await unitOfWork.ContentRepository().GetByIdAsync(id);
            if (content == null)
            {
                AddError($"Image not found with id : {id}");
                return null;
            }

            //removing it if file exists on minio
            await minioService.RemoveFileAsync(folderName: content.FolderName, content.Url);

            var result = await ProcessFileAsync(file!, content.FolderName);
            if (result is null) return null;

            var (uploadModel, contentTypeId) = result.Value;

            //updating entity
            content.Name = file!.Name;
            content.Url = uploadModel.FileName;
            content.ContentTypeId = contentTypeId;
            unitOfWork.ContentRepository().Update(content);
            await unitOfWork.SaveChangesAsync();
            return content.Id;
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return null;
        }
    }

    public async Task<int?> UpdateContentForVideo(int id, IFormFile file)
    {
        try
        {
            var content = await unitOfWork.ContentRepository().GetByIdAsync(id);
            if (content == null)
            {
                AddError($"Video not found with id {id}");
                return null;
            }
            bool isValidFile = ValdateFileForVideo(file);
            if (!isValidFile) return null;


            await minioService.RemoveFileAsync(folderName: content.FolderName, fileName: content.Url);


            var uploadModel = await PrepareAndUploadFileAsync(file!, content.FolderName);
            if (uploadModel is null) return null;


            int contentTypeId = await GetContentTypeIdAsync(uploadModel);


            content.Name = file!.Name;
            content.Url = uploadModel.FileName;
            content.ContentTypeId = contentTypeId;

            unitOfWork.ContentRepository().Update(content);
            await unitOfWork.SaveChangesAsync();

            return content.Id;
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return null;
        }
    }


    private bool ValidateFile(IFormFile? file)
    {

        if (file == null || file.Length == 0)
        {
            return false;
        }

        const long maxFileSizeInMb = 5;
        long maxSize = maxFileSizeInMb * 1024 * 1024;
        if (file.Length > maxSize)
        {
            AddError("File hajmi 5MB katta bo'lmasligi kerak");
            return false;
        }

        if (!_permittedMimeTypes.Contains(file.ContentType.ToLower()))
        {
            AddError("Faqat jpg yoki png qabul qilinadi");
            return false;
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
        {
            AddError("Faqat jpg yoki png qabul qilinadi");
            return false;
        }

        return true;
    }
    private async Task<(UploadFileModel uploadModel, int contentTypeId)?> ProcessFileAsync(IFormFile file, string folderName, bool forImg = true)
    {
        if (forImg)
        {
            bool isValidFile = ValidateFile(file);
            if (!isValidFile) return null;
        }

        var uploadModel = await GetFileDetails(file);
        await minioService.UploadFileAsync(folderName, uploadModel);
        CombineStatuses(minioService);
        if (HasErrors) return null;

        var contentTypeId = await (unitOfWork.ContentTypeRepository().GetAll())
            .Where(c => c.TypeName == uploadModel.ContentType)
            .Select(c => c.Id)
            .FirstAsync();

        return (uploadModel, contentTypeId);
    }
    private async Task<UploadFileModel> GetFileDetails(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString();
        string contentType = file.ContentType;
        long size = file.Length;

        var data = new MemoryStream();
        await file.CopyToAsync(data);
        data.Position = 0; // boshiga qaytaramiz

        return new UploadFileModel(fileName, contentType, size, data);
    }

    private bool ValdateFileForVideo(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return false;
        }

        if (!_allowedVideoTypes.Contains(file.ContentType))
        {
            AddError("Faqat video formatdagi fayllarga ruxsat beriladi!");
            return false;
        }
        return true;
    }
    private async Task<UploadFileModel?> PrepareAndUploadFileAsync(IFormFile file, string folderName)
    {
        var uploadModel = await GetFileDetails(file!);
        await minioService.UploadFileAsync(folderName, uploadModel);
        CombineStatuses(minioService);
        if (HasErrors) return null;
        return uploadModel;
    }
    private async Task<int> GetContentTypeIdAsync(UploadFileModel uploadModel)
    {
        var contentTypeId = await (unitOfWork.ContentTypeRepository().GetAll())
    .Where(c => c.TypeName == uploadModel.ContentType)
    .Select(c => c.Id)
    .FirstAsync();
        return contentTypeId;
    }
}
