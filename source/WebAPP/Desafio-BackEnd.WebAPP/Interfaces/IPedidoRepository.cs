using Desafio_BackEnd.WebAPP.Models.Moto;
using Desafio_BackEnd.WebAPP.Models.Pedido;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IPedidoRepository
    {
        Task Create(string token, CreatePedidoViewModel model);
        Task<List<PedidoViewModel>> GetAll(string token);
    }
}
