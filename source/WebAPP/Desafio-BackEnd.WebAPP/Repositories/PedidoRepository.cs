using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Pedido;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class PedidoRepository(IConfiguration configuration) : IPedidoRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task Create(string token, CreatePedidoViewModel model)
        {
            using var httpClient = new HttpClient();

            var command = new
            {
                model.DataCriacao,
                model.Valor
            };

            var json = System.Text.Json.JsonSerializer.Serialize(command);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync($"{_baseURL}/pedidos/", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task AtualizarSituacao(string token, UpdateSituacaoPedidoViewModel model)
        {
            using var httpClient = new HttpClient();

            var command = new
            {
                model.Situacao,
                model.EntregadorId
            };

            var json = System.Text.Json.JsonSerializer.Serialize(command);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PatchAsync($"{_baseURL}/pedidos/{model.PedidoId}/situacao", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<List<Pedido>> Get(string token, string? entregadorId)
        {
            using var httpClient = new HttpClient();
            List<string> param = [];

            if (!string.IsNullOrEmpty(entregadorId))
                param.Add($"entregadorId={entregadorId}");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/pedidos?{string.Join("&", param)}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Pedido>>(json)!;
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
    }
}