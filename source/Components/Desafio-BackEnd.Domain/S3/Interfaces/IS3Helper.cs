using Microsoft.AspNetCore.Http;

namespace Desafio_BackEnd.Domain.S3.Interfaces
{
    public interface IS3Helper
    {
        Task<string> UploadFile(string entregadoId, IFormFile file);
    }
}