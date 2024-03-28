using Desafio_BackEnd.WebAPP.Models.User;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> Login(string username, string password);

        Task<dynamic> Register(RegisterUserViewModel user);
    }
}