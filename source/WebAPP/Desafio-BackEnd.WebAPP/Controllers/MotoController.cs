using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Locacao;
using Desafio_BackEnd.WebAPP.Models.Moto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class MotoController(IMotoRepository motoRepository) : Controller
    {
        private readonly IMotoRepository _motoRepository = motoRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [JwtAuthorizationFilter]
        public async Task<ActionResult> MotosAvaiable(string token, DateTime dataInicio)
        {
            var result = await _motoRepository.GetAvaiable(token, dataInicio);
            return Json(result);
        }

        public ActionResult Edit(string id, string placa)
        {
            var model = new EditMotoViewModel
            {
                Id = id,
                Placa = placa
            };

            return View("Edit", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetMotos(string? placa)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                List<MotoViewModel> result = await _motoRepository.GetAll(placa, token);
                return PartialView("_MotosTablePartial", result);
            }

            return BadRequest("Token inválido");
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit([FromBody] EditMotoViewModel model)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                await _motoRepository.SaveEdit(model, token);
                return View("Index");
            }

            return BadRequest("Token inválido");
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew([FromBody] CreateMotoViewModel model)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                await _motoRepository.SaveNew(model, token);
                return View("Index");
            }

            return BadRequest("Token inválido");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                await _motoRepository.DeleteMoto(id, token);
                return View("Index");
            }

            return BadRequest("Token inválido");
        }
    }
}