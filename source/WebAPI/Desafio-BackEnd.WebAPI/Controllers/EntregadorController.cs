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
        [Route("entregadores/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EntregadorDTO))]
        public async Task<IActionResult> Get(string id)
        {
            return ResultFirst(await _entregadorRepository.GetByIdResult(id));
        }

        //[HttpGet]
        //[Route("entregadores")]
        //[ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(QueryResult<EntregadorDTO>))]
        //public async Task<IActionResult> GetAll([FromQuery] GetEntregadorQuery query)
        //{
        //    if (!query.IsValid())
        //        return Result(new CommandResult(422, query));

        //    return Result(await _entregadorRepository.GetResult(query));
        //}

        [HttpPost]
        [Route("entregadores")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(EntregadorDTO))]
        public async Task<IActionResult> Post([FromBody] InsertEntregadorCommand command)
        {
            var result = await _entregadorHandler.Handle(command);
            return ResultFirst(result);
        }

        [HttpPut]
        [Route("entregadores/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateEntregadorCommand command)
        {
            command.AlterId(id);
            var result = await _entregadorHandler.Handle(command);
            return Result(result);
        }

        //[HttpPatch]
        //[Route("entregadores/{id}/placa")]
        //public async Task<IActionResult> Patch(string id, [FromBody] UpdatePlacaCommand command)
        //{
        //    command.AlterId(id);
        //    var result = await _entregadorHandler.Handle(command);
        //    return Result(result);
        //}

        [HttpDelete]
        [Route("entregadores/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _entregadorHandler.Handle(id);
            return Result(result);
        }
    }
}