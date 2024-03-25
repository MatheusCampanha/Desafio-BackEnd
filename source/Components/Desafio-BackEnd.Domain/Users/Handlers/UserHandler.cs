using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Users.Commands;
using Desafio_BackEnd.Domain.Users.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Users.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Users.Results;
using System.Net;

namespace Desafio_BackEnd.Domain.Users.Handlers
{
    public class UserHandler(IUserRepository userRepository) : IUserHandler
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDTO> VerifyUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public async Task<Result<CreateUserCommandResult>> CreateUser(CreateUserCommand command)
        {
            var errorResult = new Result<CreateUserCommandResult>(HttpStatusCode.UnprocessableEntity.GetHashCode());

            var uniqueResult = await _userRepository.GetUserByUsername(command.Username);
            if (uniqueResult != null)
            {
                errorResult.AddNotification(nameof(command.Username), "Já cadastrado");
                return errorResult;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = new UserDTO
            {
                Username = command.Username,
                PasswordHash = passwordHash,
                Role = command.Role
            };

            return await _userRepository.CreateUser(user);
        }
    }
}