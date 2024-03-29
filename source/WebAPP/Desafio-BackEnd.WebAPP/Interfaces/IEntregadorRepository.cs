using Desafio_BackEnd.WebAPP.Models.Entregador;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IEntregadorRepository
    {
        Task<List<EntregadorViewModel>> GetAll(string token);
        Task<EditViewModel> GetById(string id, string token);
        Task Create(CreateEntregadorViewModel model, string token);
    }
}
