using Desafio_BackEnd.Domain.Core.Queries;
using Desafio_BackEnd.Domain.Motos.Commands;
using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Motos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class MotoController(IMotoHandler motoHandler, IMotoRepository motoRepository) : BaseController
    {
        private readonly IMotoHandler _motoHandler = motoHandler;
        private readonly IMotoRepository _motoRepository = motoRepository;

        [HttpGet]
        [Route("motos")]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(List<MotoDTO>))]
        public async Task<IActionResult> GetAll(string? placa)
        {
            var result = await _motoRepository.GetResult(placa);

            if (result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("motos/disponiveis/")]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(List<MotoDTO>))]
        public async Task<IActionResult> GetAvaiable()
        {
            var result = await _motoRepository.GetAvaiable();

            if (result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("motos")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(MotoDTO))]
        public async Task<IActionResult> Post([FromBody] InsertMotoCommand command)
        {
            var result = await _motoHandler.Handle(command);
            return ResultFirst(result);
        }

        [HttpPatch]
        [Route("motos/{id}/placa")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdatePlacaCommand command)
        {
            command.AlterId(id);
            var result = await _motoHandler.Handle(command);
            return Result(result);
        }

        [HttpDelete]
        [Route("motos/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _motoHandler.Handle(id);
            return Result(result);
        }
    }
}