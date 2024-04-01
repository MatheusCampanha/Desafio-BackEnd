using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Pedidos.Commands;
using Desafio_BackEnd.Domain.Pedidos.DTO;

namespace Desafio_BackEnd.Domain.Pedidos.Interfaces.Handlers
{
    public interface IPedidoHandler
    {
        Task<Result<PedidoDTO>> Handle(CreatePedidoCommand command);

        Task<CommandResult> Handle(UpdateSituacaoPedidoCommand command);

        Task<CommandResult> Handle(string id);
    }
}