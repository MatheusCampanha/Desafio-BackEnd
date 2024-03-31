using Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class LocacaoController(IEntregadorHandler entregadorHandler, IEntregadorRepository entregadorRepository) : BaseController
    {
    }
}
