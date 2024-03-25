using Desafio_BackEnd.Domain.Core.Enums;
using MongoDB.Bson;

namespace Desafio_BackEnd.Domain.Users
{
    public class UserDTO
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public RoleUserEnum Role { get; set; }
    }
}