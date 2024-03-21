namespace Desafio_BackEnd.Infra.Data.Helpers
{
    public interface IS3Helper
    {
        Task<string> Upload(string bucketName, string key, Stream imageStream, string contentType);
        Task Delete(string bucketName, string key);
        Task Download(string bucketName, string key, Stream outputStream);
    }
}