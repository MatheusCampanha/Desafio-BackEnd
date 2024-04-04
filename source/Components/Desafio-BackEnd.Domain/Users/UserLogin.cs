namespace Desafio_BackEnd.Domain.Users
{
    public class UserLogin(string username, string password)
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
    }
}