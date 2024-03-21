using Desafio_BackEnd.WebAPP.Models.Moto;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class MotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(string id, string placa)
        {
            var model = new EditarMotoViewModel
            {
                Id = id,
                Placa = placa
            };

            return View(model);
        }
    }
}