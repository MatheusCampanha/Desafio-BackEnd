using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Entregador;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class EntregadorRepository(IConfiguration configuration) : IEntregadorRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task Save(SaveEntregadorViewModel model, IFormFile imagemCNH, string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = new HttpResponseMessage();
            if (string.IsNullOrEmpty(model.Id))
            {
                var command = new
                {
                    model.UserId,
                    model.Nome,
                    model.Cnpj,
                    model.DataNascimento,
                    model.NumeroCNH,
                    model.TipoCNH
                };

                var json = System.Text.Json.JsonSerializer.Serialize(command);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                response = await httpClient.PostAsync($"{_baseURL}/entregadores", content);
            }
            else
            {
                var formData = new MultipartFormDataContent();

                if (imagemCNH != null)
                {
                    var imageContent = new StreamContent(imagemCNH.OpenReadStream());
                    formData.Add(imageContent, "imagemCNH", imagemCNH.FileName);
                }

                response = await httpClient.PatchAsync($"{_baseURL}/entregadores/{model.Id}/imagemCNH", formData);
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<List<EntregadorViewModel>> GetAll(string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/entregadores");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EntregadorViewModel>>(json)!;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return [];
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<EntregadorViewModel> GetById(string id, string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/entregadores/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EntregadorViewModel>(json)!;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<EntregadorViewModel> GetByUserId(string userId, string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/entregadores/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EntregadorViewModel>(json)!;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }
    }
}