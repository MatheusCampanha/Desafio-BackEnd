using Desafio_BackEnd.WebAPP.Models.Entregador;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IEntregadorRepository
    {
        Task Create(CreateEntregadorViewModel model, string token);
    }
}
