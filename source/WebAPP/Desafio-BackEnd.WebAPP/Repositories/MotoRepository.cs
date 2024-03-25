using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Motos.Queries;
using Desafio_BackEnd.WebAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class MotoRepository(IConfiguration configuration) : IMotoRepository
    {
        private readonly string _baseURL = configuration["Services.UrlAPI"];

        public async Task<MotoDTO> GetAll([FromQuery] GetMotoQuery query, string token)
        {
            using var httpClient = new HttpClient();
            List<string> param = [];

            if (!string.IsNullOrEmpty(query.Placa))
                param.Add($"Placa={query.Placa}");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/motos?{string.Join("&", param)}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MotoDTO>();
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