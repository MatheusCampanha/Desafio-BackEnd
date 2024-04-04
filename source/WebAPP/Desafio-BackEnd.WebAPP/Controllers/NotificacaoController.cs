using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Notificacao;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class NotificacaoController(INotificacaoRepository notificacaoRepository) : Controller
    {
        private readonly INotificacaoRepository _notificacaoRepository = notificacaoRepository;

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Index(string token)
        {
            var model = new NotificacaoViewModel();

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);

            var claims = jwt.Claims;
            var isUserAdmin = claims.Any(c => c.Value == "Admin");

            string? entregadorId = null;

            if (!isUserAdmin)
                entregadorId = GetUserId(token);

            model.IsUserAdmin = isUserAdmin;
            model.Notificacoes = await _notificacaoRepository.Get(token, entregadorId);

            return View(model);
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
