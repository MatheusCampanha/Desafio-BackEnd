using Desafio_BackEnd.Domain.Notificacoes.DTO;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    public class NotificacaoController(INotificacaoRepository notificacaoRepository) : ControllerBase
    {
        private readonly INotificacaoRepository _notificacaoRepository = notificacaoRepository;

        [HttpGet]
        [Route("notificacoes")]
        [ProducesResponseType((int)HttpStatusCode.PartialContent, Type = typeof(List<NotificacaoDTO>))]
        public async Task<IActionResult> Get(string? entregadorId)
        {
            var result = await _notificacaoRepository.Get(entregadorId);

            if (result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }
    }
}