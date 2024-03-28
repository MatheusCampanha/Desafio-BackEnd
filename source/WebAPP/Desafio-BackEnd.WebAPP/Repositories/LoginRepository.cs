using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.User;
using Newtonsoft.Json;

namespace Desafio_BackEnd.WebAPP.Repositories
{
    public class LoginRepository(IConfiguration configuration) : ILoginRepository
    {
        private readonly string _baseURL = configuration["Services:UrlAPI"];

        public async Task<string> Login(string username, string password)
        {
            using var httpClient = new HttpClient();
            var login = new
            {
                Username = username,
                Password = password
            };

            var jsonData = JsonConvert.SerializeObject(login);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_baseURL}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Status code: {response.StatusCode}, Error: {errorContent}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<dynamic> Register(RegisterUserViewModel user)
        {
            using var httpClient = new HttpClient();
            var body = new
            {
                user.Username,
                user.Password,
                user.Role
            };

            var jsonData = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_baseURL}/register", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return new { response.StatusCode, result };
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