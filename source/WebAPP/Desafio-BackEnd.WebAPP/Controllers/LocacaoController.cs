using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Locacao;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class LocacaoController(IEntregadorRepository entregadorRepository) : Controller
    {
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Index(string token)
        {
            var userId = GetUserId(token);
            userId = "66062139c16619a6c5252c0f";
            var entregador = await _entregadorRepository.GetById(userId, token);
            return View(entregador);
        }

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Rent(LocacaoViewModel model)
        {
            var teste = "";
            return View("index");
        }

        private static string GetUserId(string? jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var claims = token.Claims;

            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim!.Value;
        }
    }
}