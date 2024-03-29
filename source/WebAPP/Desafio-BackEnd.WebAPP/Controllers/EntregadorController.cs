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

        public async Task<IActionResult> Edit(string entregadorId)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                var entregador = await _entregadorRepository.GetById(entregadorId, token);
                return View(entregador);
            }

            return BadRequest("Token inválido");
        }

        [HttpGet]
        public async Task<ActionResult> GetEntregadores()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                List<EntregadorViewModel> result = await _entregadorRepository.GetAll(token);
                return PartialView("_EntregadoresTablePartial", result);
            }

            return BadRequest("Token inválido");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEntregadorViewModel model)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWTToken");
                if (!string.IsNullOrEmpty(token))
                {
                    await _entregadorRepository.Create(model, token);
                }

                return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}