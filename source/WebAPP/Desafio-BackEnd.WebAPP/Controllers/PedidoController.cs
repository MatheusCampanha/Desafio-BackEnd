using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Pedido;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class PedidoController(IPedidoRepository pedidoRepository, IEntregadorRepository entregadorRepository) : Controller
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Index(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);

            var claims = jwt.Claims;
            var isUserAdmin = claims.Any(c => c.Value == "Admin");

            string? entregadorId = null;

            if (!isUserAdmin)
            {
                var userId = GetUserId(token);
                var entregador = await _entregadorRepository.GetByUserId(userId, token);
                if (entregador == null)
                    return RedirectToAction("Create", "Entregador", token);

                entregadorId = entregador.Id;
            }
            var model = new PedidoViewModel();

            var pedidos = await _pedidoRepository.Get(token, entregadorId);

            model.IsUserAdmin = isUserAdmin;
            model.Pedidos = pedidos;

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthorizationFilter]
        public async Task<IActionResult> Create(string token, [FromBody] CreatePedidoViewModel model)
        {
            try
            {
                await _pedidoRepository.Create(token, model);
                return Json(new { status = "Ok", message = "Pedido criado!" });
            }
            catch
            {
                return Json(new { status = "Error", message = "Falha interna..." });
            }
        }

        [HttpPost]
        [JwtAuthorizationFilter]
        public async Task<IActionResult> UpdateSitacao(string token, [FromForm] UpdateSituacaoPedidoViewModel model)
        {
            try
            {
                await _pedidoRepository.AtualizarSituacao(token, model);
                return Json(new { status = "Ok", message = "Confirmado!" });
            }
            catch
            {
                return Json(new { status = "Error", message = "Falha interna..." });
            }
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
