using Desafio_BackEnd.WebAPP.Models.Locacao;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface ILocacaoRepository
    {
        Task Create(LocacaoViewModel model, string token);
    }
}