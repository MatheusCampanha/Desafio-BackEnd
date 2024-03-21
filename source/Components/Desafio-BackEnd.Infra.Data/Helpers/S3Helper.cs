using Amazon.S3;
using Amazon.S3.Model;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Helpers
{
    public class S3Helper : IS3Helper
    {
        private readonly IAmazonS3 _s3Client;

        public S3Helper(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task Download(string bucketName, string key, Stream outputStream)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            using var response = await _s3Client.GetObjectAsync(request);
            await response.ResponseStream.CopyToAsync(outputStream);
        }

        public async Task Delete(string bucketName, string key)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteObjectRequest);
        }

        public async Task<string> Upload(string bucketName, string key, Stream imageStream, string contentType)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = imageStream,
                ContentType = contentType
            };

            var response = await _s3Client.PutObjectAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.OK)
                return $"https://{bucketName}.s3.amazonaws.com/{key}";
            else
                throw new Exception("Failed to upload image to Amazon S3");
        }
    }
}