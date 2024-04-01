using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class PedidoController(IPedidoRepository pedidoRepository) : Controller
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Index(string token)
        {
            var model = await _pedidoRepository.GetAll(token);
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
                return Json(new { status = "Error", message = "Falha ao realizar pedido!" });
            }
        }
    }
}
