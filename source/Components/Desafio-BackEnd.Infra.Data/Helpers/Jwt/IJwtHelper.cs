namespace Desafio_BackEnd.Infra.Data.Helpers.Jwt
{
    public interface IJwtHelper
    {
        string GenerateToken(string id, string role);
    }
}