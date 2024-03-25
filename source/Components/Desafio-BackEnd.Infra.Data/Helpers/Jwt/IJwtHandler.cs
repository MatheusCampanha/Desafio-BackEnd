namespace Desafio_BackEnd.Infra.Data.Helpers.Jwt
{
    public interface IJwtHandler
    {
        string GenerateToken(string username);
    }
}