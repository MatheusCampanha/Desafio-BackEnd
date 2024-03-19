using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Queries;
using Desafio_BackEnd.Domain.Motos.Commands;
using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Motos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Motos.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class MotoController(IMotoHandler motoHandler, IMotoRepository motoRepository) : BaseController
    {
        private readonly IMotoHandler _motoHandler = motoHandler;
        private readonly IMotoRepository _motoRepository = motoRepository;

        [HttpGet]
        [Route("motos/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MotoDTO))]
        public async Task<IActionResult> Get(string id)
        {
            return ResultFirst(await _motoRepository.GetByIdResult(id));
        }

        [HttpGet]
        [Route("motos")]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(QueryResult<MotoDTO>))]
        public async Task<IActionResult> GetAll([FromQuery] GetMotoQuery query)
        {
            if (!query.IsValid())
                return Result(new CommandResult(422, query));

            return Result(await _motoRepository.GetResult(query));
        }

        [HttpPost]
        [Route("motos")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(MotoDTO))]
        public async Task<IActionResult> Post(InsertMotoCommand command)
        {
            var result = await _motoHandler.Handle(command);
            return ResultFirst(result);
        }

        [HttpPut]
        [Route("motos/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateMotoCommand command)
        {
            command.AlterId(id);
            var result = await _motoHandler.Handle(command);
            return Result(result);
        }

        [HttpPatch]
        [Route("motos/{id}/placa")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdatePlacaCommand command)
        {
            command.AlterId(id);
            var result = await _motoHandler.Handle(command);
            return Result(result);
        }

        [HttpDelete]
        [Route("motos/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _motoHandler.Handle(id);
            return Result(result);
        }
    }
}
