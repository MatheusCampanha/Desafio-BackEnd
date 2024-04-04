using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Users;
using Desafio_BackEnd.Domain.Users.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Users.Results;
using MongoDB.Driver;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDTO> _users;

        public UserRepository(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _users = database.GetCollection<UserDTO>("User");
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<Result<CreateUserCommandResult>> CreateUser(UserDTO user)
        {
            await _users.InsertOneAsync(user);

            return new Result<CreateUserCommandResult>(System.Net.HttpStatusCode.Created.GetHashCode(), new CreateUserCommandResult(user.Username, user.Role));
        }
    }
}