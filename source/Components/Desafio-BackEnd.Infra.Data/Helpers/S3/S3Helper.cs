using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.S3.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Desafio_BackEnd.Infra.Data.Helpers.S3
{
    public class S3Helper(Settings settings, IAmazonS3 s3Client) : IS3Helper
    {
        private readonly Settings _settings = settings;
        private readonly IAmazonS3 _s3Client = s3Client;

        public async Task<string> UploadFile(string entregadorId, IFormFile file)
        {
            try
            {
                var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _settings.S3Settings.BucketName);
                if (!bucketExists)
                    await _s3Client.PutBucketAsync(_settings.S3Settings.BucketName);

                var fileName = entregadorId + "/" + Path.GetFileName(file.FileName);
                var request = new PutObjectRequest()
                {
                    BucketName = _settings.S3Settings.BucketName,
                    Key = fileName,
                    InputStream = file.OpenReadStream()
                };

                request.Metadata.Add("Content-Type", file.ContentType);
                await _s3Client.PutObjectAsync(request);

                var filePathRequest = new GetPreSignedUrlRequest
                {
                    BucketName = _settings.S3Settings.BucketName,
                    Key = fileName,
                    Expires = DateTime.Now.AddHours(1)
                };

                var filePath = await _s3Client.GetPreSignedURLAsync(filePathRequest);

                return filePath;
            }
            catch
            {
                throw;
            }
        }
    }
}