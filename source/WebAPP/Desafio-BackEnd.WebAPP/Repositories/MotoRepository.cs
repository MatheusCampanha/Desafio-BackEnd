using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Moto;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class MotoRepository(IConfiguration configuration) : IMotoRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task<List<MotoViewModel>> GetAll(string placa, string token)
        {
            using var httpClient = new HttpClient();
            List<string> param = [];

            if (!string.IsNullOrEmpty(placa))
                param.Add($"Placa={placa}");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/motos?{string.Join("&", param)}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MotoViewModel>>(json)!;
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

        public async Task<List<MotoViewModel>> GetAvaiable(string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/motos/disponiveis");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MotoViewModel>>(json)!;
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

        public async Task DeleteMoto(string id, string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync($"{_baseURL}/motos/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task SaveEdit(EditMotoViewModel model, string token)
        {
            using var httpClient = new HttpClient();

            var command = new
            {
                model.Placa
            };

            var json = System.Text.Json.JsonSerializer.Serialize(command);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PatchAsync($"{_baseURL}/motos/{model.Id}/placa", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task SaveNew(CreateMotoViewModel model, string token)
        {
            using var httpClient = new HttpClient();

            var command = new
            {
                model.Ano,
                model.Modelo,
                model.Placa
            };

            var json = System.Text.Json.JsonSerializer.Serialize(command);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync($"{_baseURL}/motos/", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }
    }
}