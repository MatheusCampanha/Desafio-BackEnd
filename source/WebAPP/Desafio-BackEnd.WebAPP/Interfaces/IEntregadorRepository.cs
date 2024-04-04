using Desafio_BackEnd.WebAPP.Models.Entregador;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IEntregadorRepository
    {
        Task<List<EntregadorViewModel>> GetAll(string token);

        Task<EntregadorViewModel> GetById(string id, string token);

        Task<EntregadorViewModel> GetByUserId(string userId, string token);

        Task Save(SaveEntregadorViewModel model, IFormFile imagemCNH, string token);
    }
}