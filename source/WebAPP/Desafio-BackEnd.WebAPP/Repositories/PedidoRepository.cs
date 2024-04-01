using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Moto;
using Desafio_BackEnd.WebAPP.Models.Pedido;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;

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

        public async Task<List<PedidoViewModel>> GetAll(string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/pedidos");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PedidoViewModel>>(json)!;
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
