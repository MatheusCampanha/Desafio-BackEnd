using Desafio_BackEnd.WebAPP.Models.Locacao;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface ILocacaoRepository
    {
        Task Create(LocacaoViewModel model, string token);
        Task EndRate(string id, string token);
        Task<LocacaoViewModel> GetActive(string id, string token);
    }
}