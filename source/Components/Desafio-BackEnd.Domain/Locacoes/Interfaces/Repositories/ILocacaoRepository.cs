using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Locacoes.DTO;

namespace Desafio_BackEnd.Domain.Locacoes.Interfaces.Repositories
{
    public interface ILocacaoRepository
    {
        Task<Result<LocacaoDTO>> Insert(LocacaoDTO locacao);
        Task EndRate(string id);
        Task<LocacaoDTO> GetActive(string id);
    }
}