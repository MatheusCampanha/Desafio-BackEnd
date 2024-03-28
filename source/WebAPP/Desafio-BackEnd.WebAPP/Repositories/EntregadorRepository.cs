using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Entregador;
using System.Net.Http.Headers;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class EntregadorRepository(IConfiguration configuration) : IEntregadorRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task Create(CreateEntregadorViewModel model, string token)
        {
            using var httpClient = new HttpClient();

            var (base64String, nomeArquivo) = await ConvertImageToBase64(model.ImagemCNH);

            var command = new
            {
                model.Nome,
                model.Cnpj,
                model.DataNascimento,
                model.NumeroCNH,
                model.TipoCNH,
                ImagemBase64 = base64String,
                NomeArquivo = nomeArquivo
            };

            var json = System.Text.Json.JsonSerializer.Serialize(command);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync($"{_baseURL}/entregadores", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<(string? base64String, string? nomeArquivo)> ConvertImageToBase64(IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
            {
                return (null, null);
            }

            string nomeArquivo = imagem.FileName;

            using var ms = new MemoryStream();
            await imagem.CopyToAsync(ms);
            byte[] bytes = ms.ToArray();
            string base64String = Convert.ToBase64String(bytes);
            return (base64String, nomeArquivo);
        }
    }
}
