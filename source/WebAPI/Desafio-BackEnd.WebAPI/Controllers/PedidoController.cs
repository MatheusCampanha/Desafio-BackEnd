using Desafio_BackEnd.Domain.Pedidos.Commands;
using Desafio_BackEnd.Domain.Pedidos.DTO;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class PedidoController(IPedidoHandler pedidoHandler, IPedidoRepository pedidoRepository) : BaseController
    {
        private readonly IPedidoHandler _pedidoHandler = pedidoHandler;
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        [HttpGet]
        [Route("pedidos")]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(List<PedidoDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _pedidoRepository.GetResult();
            if (result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("pedidos")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(PedidoDTO))]
        public async Task<IActionResult> Post([FromBody] CreatePedidoCommand command)
        {
            var result = await _pedidoHandler.Handle(command);
            return ResultFirst(result);
        }

        [HttpPatch]
        [Route("pedidos/{id}/situacao")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdateSituacaoPedidoCommand command)
        {
            command.SetId(id);
            var result = await _pedidoHandler.Handle(command);
            return Result(result);
        }

        [HttpDelete]
        [Route("pedidos/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _pedidoHandler.Handle(id);
            return Result(result);
        }
    }
}