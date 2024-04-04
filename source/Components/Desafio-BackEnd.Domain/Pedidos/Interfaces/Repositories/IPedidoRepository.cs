using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Pedidos.DTO;

namespace Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task<Result<Pedido>> GetById(string id);

        Task<List<PedidoDTO>> GetResult();

        Task<List<PedidoDTO>> GetUnfinished();

        Task<Result<PedidoDTO>> Insert(PedidoDTO pedido);

        Task<CommandResult> Update(PedidoDTO pedido);

        Task<CommandResult> Delete(string id);
    }
}