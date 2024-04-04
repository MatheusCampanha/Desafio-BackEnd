using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Users.Commands;
using Desafio_BackEnd.Domain.Users.Results;

namespace Desafio_BackEnd.Domain.Users.Interfaces.Handlers
{
    public interface IUserHandler
    {
        Task<UserDTO> VerifyUser(string username, string password);

        Task<Result<CreateUserCommandResult>> CreateUser(CreateUserCommand command);
    }
}