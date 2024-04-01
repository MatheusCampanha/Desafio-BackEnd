using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Locacao;
using Desafio_BackEnd.WebAPP.Models.Moto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class LocacaoRepository(IConfiguration configuration) : ILocacaoRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task Create(LocacaoViewModel model, string token)
        {
            using var httpClient = new HttpClient();

            var command = new
            {
                model.EntregadorId,
                model.MotoId,
                model.DataInicial,
                model.DataFinal,
                model.DataPrevisaoEntrega,
                model.ValorTotal,
                model.Plano,
                model.Finalizada
            };

            var json = System.Text.Json.JsonSerializer.Serialize(command);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync($"{_baseURL}/locacoes/", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task EndRate(string id, string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PatchAsync($"{_baseURL}/locacoes/{id}/finalizar", null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<LocacaoViewModel> GetActive(string id, string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/locacoes/entregador/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LocacaoViewModel>(json)!;
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
