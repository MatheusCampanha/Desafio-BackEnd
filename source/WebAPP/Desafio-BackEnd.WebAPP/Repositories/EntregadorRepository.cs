using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Entregador;
using Desafio_BackEnd.WebAPP.Models.Moto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class EntregadorRepository(IConfiguration configuration) : IEntregadorRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task Create(CreateEntregadorViewModel model, string token)
        {
            using var httpClient = new HttpClient();

            var command = new
            {
                model.Nome,
                model.Cnpj,
                model.DataNascimento,
                model.NumeroCNH,
                model.TipoCNH
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

        public async Task<EditViewModel> GetById(string id, string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/entregadores/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<EntregadorDTO>(json)!;
                return new EditViewModel{
                    Id = result.Id,
                    Nome = result.Nome,
                    CaminhoImagem = result.CaminhoImagemCNH
                };
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