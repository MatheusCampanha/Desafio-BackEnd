using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Notificacao;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class NotificacaoRepository(IConfiguration configuration) : INotificacaoRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task<List<Notificacao>> Get(string token, string? entregadorId)
        {
            using var httpClient = new HttpClient();
            List<string> param = [];

            if (!string.IsNullOrEmpty(entregadorId))
                param.Add($"entregadorId={entregadorId}");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync($"{_baseURL}/notificacoes?{string.Join("&", param)}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Notificacao>>(json)!;
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