using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.Commands;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Microsoft.AspNetCore.Http;

namespace Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers
{
    public interface IEntregadorHandler
    {
        Task<Result<EntregadorDTO>> Handle(InsertEntregadorCommand command);

        Task<CommandResult> Handle(string id, IFormFile imagemCNH);

        Task<CommandResult> Handle(string id);
    }
}