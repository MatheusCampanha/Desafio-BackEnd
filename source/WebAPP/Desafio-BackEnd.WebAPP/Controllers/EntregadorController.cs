using Desafio_BackEnd.Domain.Entregadores;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEntregadorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Salvar os dados do entregador no banco de dados
                    // Exemplo de como salvar a imagem no servidor:
                    //if (model.ImagemCNH != null && model.ImagemCNH.Length > 0)
                    //{
                    //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImagemCNH);
                    //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\entregadores", fileName);

                    //    using (var stream = new FileStream(filePath, FileMode.Create))
                    //    {
                    //        model.ImagemCNH.CopyTo(stream);
                    //    }

                    //    model.ImagemCNH = fileName; // Salva o nome do arquivo no modelo
                    //}
                    var token = HttpContext.Session.GetString("JWTToken");
                    if (!string.IsNullOrEmpty(token))
                    {
                        await _entregadorRepository.Create(model, token);
                    }
                    // Aqui você deveria chamar o serviço ou repositório para salvar o entregador no banco de dados
                    // Por exemplo:
                    // _entregadorService.Save(model);

                    return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
                }

                return View(model);
            }
            catch(Exception )
            {
                return View(model);
            }
        }
    }
}
