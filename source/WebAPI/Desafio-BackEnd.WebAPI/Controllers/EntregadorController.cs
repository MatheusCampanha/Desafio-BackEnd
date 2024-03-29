using Desafio_BackEnd.Domain.Core.Queries;
using Desafio_BackEnd.Domain.Entregadores.Commands;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class EntregadorController(IEntregadorHandler entregadorHandler, IEntregadorRepository entregadorRepository) : BaseController
    {
        private readonly IEntregadorHandler _entregadorHandler = entregadorHandler;
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        [HttpGet]
        [Route("entregadores")]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(List<EntregadorDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _entregadorRepository.GetAll();

            if (result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("entregadores/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EntregadorDTO))]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _entregadorRepository.GetByIdResult(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("entregadores")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(EntregadorDTO))]
        public async Task<IActionResult> Post([FromBody] InsertEntregadorCommand command)
        {
            var result = await _entregadorHandler.Handle(command);
            return ResultFirst(result);
        }

        [HttpPatch]
        [Route("entregadores/{id}/imagemCNH")]
        public async Task<IActionResult> Patch(string id, [FromBody] IFormFile imagemCNH)
        {
            var result = await _entregadorHandler.Handle(id, imagemCNH);
            return Result(result);
        }


        [HttpDelete]
        [Route("entregadores/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _entregadorHandler.Handle(id);
            return Result(result);
        }
    }
}