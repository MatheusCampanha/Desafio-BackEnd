using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Entregador;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class EntregadorController(IEntregadorRepository entregadorRepository) : Controller
    {
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        public IActionResult Index()
        {
            return View();
        }

        [JwtAuthorizationFilter]
        public async Task<IActionResult> Create(string token)
        {
            var model = new EntregadorViewModel();

            var userId = GetUserId(token);
            var entregador = await _entregadorRepository.GetByUserId(userId, token);

            model.UserId = userId;

            if (entregador != null)
            {
                model.Id = entregador.Id;
                model.UserId = userId;
                model.Nome = entregador.Nome;
                model.Cnpj = entregador.Cnpj;
                model.DataNascimento = entregador.DataNascimento;
                model.NumeroCNH = entregador.NumeroCNH;
                model.TipoCNH = entregador.TipoCNH;
                model.CaminhoImagemCNH = entregador.CaminhoImagemCNH;
            }

            return View(model);
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
        public async Task<ActionResult> GetByUserId(string token, string userId)
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
        public async Task<IActionResult> Save(string token, [FromForm] SaveEntregadorViewModel model, IFormFile imagemCNH)
        {
            try
            {
                await _entregadorRepository.Save(model, imagemCNH, token);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View(model);
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