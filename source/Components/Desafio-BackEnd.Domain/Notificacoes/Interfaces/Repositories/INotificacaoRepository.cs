using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Notificacoes.DTO;

namespace Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories
{
    public interface INotificacaoRepository
    {
        Task<Result<NotificacaoDTO>> Create(NotificacaoDTO notificao);

        Task<List<NotificacaoDTO>> Get(string? entregadorId);
    }
}