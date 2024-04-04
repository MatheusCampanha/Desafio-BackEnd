using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Locacao;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class LocacaoController(ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository) : Controller
    {
        private readonly ILocacaoRepository _locacaoRepository = locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Index(string token)
        {
            var userId = GetUserId(token);
            var entregador = await _entregadorRepository.GetByUserId(userId, token);

            if (entregador == null)
                return RedirectToAction("Create", "Entregador");

            var locacaoAtiva = await _locacaoRepository.GetActive(entregador.Id, token);
            if (locacaoAtiva != null)
                return View(locacaoAtiva);
            else
                return View(new LocacaoViewModel { EntregadorId = entregador.Id });
        }

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Rent(string token, LocacaoViewModel model)
        {
            try
            {
                await _locacaoRepository.Create(model, token);
                return Json(new {status = "Ok", message = "Locação confirmada!"});
            }
            catch
            {
                return Json(new { status = "Error", message = "Falha ao realizar confirmada!" });
            }
        }

        [JwtAuthorizationFilter]
        public async void EndRent(string token, string id)
        {
            await _locacaoRepository.EndRate(id, token);
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