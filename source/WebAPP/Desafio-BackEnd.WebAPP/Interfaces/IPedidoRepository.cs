using Desafio_BackEnd.WebAPP.Models.Pedido;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IPedidoRepository
    {
        Task Create(string token, CreatePedidoViewModel model);

        Task AtualizarSituacao(string token, UpdateSituacaoPedidoViewModel model);

        Task<List<Pedido>> Get(string token, string? entregadorId);
    }
}