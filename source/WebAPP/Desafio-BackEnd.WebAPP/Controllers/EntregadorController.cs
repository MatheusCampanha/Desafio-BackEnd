using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Entregador;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class EntregadorController(IEntregadorRepository entregadorRepository) : Controller
    {
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Edit(string token, string entregadorId)
        {
            var entregador = await _entregadorRepository.GetById(entregadorId, token);
            return View(entregador);
        }

        [HttpGet]
        [JwtAuthorizationFilter]
        public async Task<ActionResult> GetEntregadores(string token)
        {
            List<EntregadorViewModel> result = await _entregadorRepository.GetAll(token);
            return PartialView("_EntregadoresTablePartial", result);
        }

        [HttpGet]
        [JwtAuthorizationFilter]
        public async Task<ActionResult> EntregadorCanRate(string token, string entregadorId)
        {
            var entregador = await _entregadorRepository.GetById(entregadorId, token);
            if (entregador.TipoCNH.Equals("A"))
                return Json(true);
            else
                return Json(false);
        }

        [HttpPost]
        [JwtAuthorizationFilter]
        public async Task<IActionResult> Create(string token, [FromForm] CreateEntregadorViewModel model)
        {
            try
            {
                await _entregadorRepository.Create(model, token);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}