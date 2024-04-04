using Desafio_BackEnd.WebAPP.Configurations.Filters;
using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.Moto;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> MotosAvaiable(string token)
        {
            var result = await _motoRepository.GetAvaiable(token);
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
        [JwtAuthorizationFilter]
        public async Task<ActionResult> GetMotos(string token, string? placa)
        {
            List<MotoViewModel> result = await _motoRepository.GetAll(placa, token);
            return PartialView("_MotosTablePartial", result);
        }

        [HttpPost]
        [JwtAuthorizationFilter]
        public async Task<IActionResult> SaveEdit(string token, [FromBody] EditMotoViewModel model)
        {
            await _motoRepository.SaveEdit(model, token);
            return View("Index");
        }

        [HttpPost]
        [JwtAuthorizationFilter]
        public async Task<IActionResult> SaveNew(string token, [FromBody] CreateMotoViewModel model)
        {
            await _motoRepository.SaveNew(model, token);
            return View("Index");
        }

        [HttpDelete]
        [JwtAuthorizationFilter]
        public async Task<IActionResult> Delete(string token, string id)
        {
            await _motoRepository.DeleteMoto(id, token);
            return View("Index");
        }
    }
}