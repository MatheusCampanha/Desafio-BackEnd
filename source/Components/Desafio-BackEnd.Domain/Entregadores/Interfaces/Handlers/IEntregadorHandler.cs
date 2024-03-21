using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.Commands;
using Desafio_BackEnd.Domain.Entregadores.DTO;

namespace Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers
{
    public interface IEntregadorHandler
    {
        Task<Result<EntregadorDTO>> Handle(InsertEntregadorCommand command);

        Task<CommandResult> Handle(UpdateEntregadorCommand command);

        Task<CommandResult> Handle(string id);
    }
}