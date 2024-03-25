using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Users.Results;

namespace Desafio_BackEnd.Domain.Users.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserByUsername(string username);

        Task<Result<CreateUserCommandResult>> CreateUser(UserDTO user);
    }
}