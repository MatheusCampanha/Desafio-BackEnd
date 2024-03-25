using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;

namespace Desafio_BackEnd.Domain.Users.Commands
{
    public class CreateUserCommand(string username, string password, RoleUserEnum role) : Command
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public RoleUserEnum Role { get; private set; } = role;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}