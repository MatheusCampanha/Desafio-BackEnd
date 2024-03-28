using Desafio_BackEnd.Domain.Core.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desafio_BackEnd.Infra.Data.Helpers.Jwt
{
    public class JwtHelper(Settings settings) : IJwtHelper
    {
        private readonly Settings _settings = settings;

        public string GenerateToken(string username, string role)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Jwt.SecretKey));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                claims:
                [
                    new Claim(type: ClaimTypes.Name, username),
                    new Claim(type: ClaimTypes.Role, role)
                ],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}