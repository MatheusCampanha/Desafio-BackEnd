using Desafio_BackEnd.Domain.Core.Enums;

namespace Desafio_BackEnd.Domain.Users.Results
{
    public class CreateUserCommandResult(string username, RoleUserEnum role)
    {
        public string Username { get; private set; } = username;
        public RoleUserEnum Role { get; private set; } = role;
    }
}