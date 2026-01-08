using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using OnlineCourse.Common.Settings;
using StatusGeneric;

namespace OnlineCourse.Service.Infrastructure;

public class MinioService : StatusGenericHandler, IMinioService
{
    private readonly MinioClient _minioClient;
    private readonly string _bucketName;

    public MinioService(IOptions<MinioSetting> options)
    {
        var settings = options.Value;
        _bucketName = settings.BucketName;

        var client = new MinioClient()
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey);

        if (settings.Secure)
            client = client.WithSSL();

        _minioClient = (MinioClient)new MinioClient()
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .Build();
    }
    public async Task UploadFileAsync(string folderName, UploadFileModel file)
    {
        try
        {
            bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!found)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
            }

            var objectName = string.IsNullOrWhiteSpace(folderName)
                ? file.FileName
                : $"{folderName.TrimEnd('/')}/{file.FileName}";

            if (file.Data.CanSeek)
            {
                file.Data.Position = 0; // boshiga qaytarish
            }

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithStreamData(file.Data)
                .WithObjectSize(file.Size)
                .WithContentType(file.ContentType));

            Message = $"File '{file.FileName}' uploaded successfully.";
        }
        catch (MinioException e)
        {
            AddError($"[MinIO Upload Error]: {e.Message}");
        }
    }

    public async Task<UploadFileModel?> GetFileAsync(string folderName, string fileName)
    {
        try
        {
            var objectName = string.IsNullOrWhiteSpace(folderName)
                ? fileName
                : $"{folderName.TrimEnd('/')}/{fileName}";

            var memoryStream = new MemoryStream();

            string contentType = "application/octet-stream"; // fallback

            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

            memoryStream.Position = 0;

            Message = $"File '{fileName}' retrieved successfully.";
            return new UploadFileModel(fileName, contentType, memoryStream.Length, memoryStream);
        }
        catch (ObjectNotFoundException)
        {
            AddError($"File topilmadi. FileId : {fileName}");
            return null;
        }
        catch (MinioException e)
        {
            throw new MinioException($"[Minio Error]: {e.Message}");
        }
    }

    public async Task RemoveFileAsync(string folderName, string fileName)
    {
        try
        {
            var objectName = string.IsNullOrWhiteSpace(folderName)
                ? fileName
                : $"{folderName.TrimEnd('/')}/{fileName}";

            //checking it exists
            var memoryStream = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName));

            Message = $"File '{fileName}' removed successfully.";
        }
        catch (ObjectNotFoundException)
        {
            // agar topilmasa shuncaki return qlb quyadi va qolgan ishlarda davom etadi
        }
        catch (Exception e)
        {
            throw new MinioException($"[MinIO Remove Error for {fileName}]: {e.Message}");
        }
    }
}
