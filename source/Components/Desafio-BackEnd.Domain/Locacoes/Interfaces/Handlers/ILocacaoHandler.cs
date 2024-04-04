using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Locacoes.Commands;
using Desafio_BackEnd.Domain.Locacoes.DTO;

namespace Desafio_BackEnd.Domain.Locacoes.Interfaces.Handlers
{
    public interface ILocacaoHandler
    {
        Task<Result<LocacaoDTO>> Handle(InsertLocacaoCommand command);
    }
}