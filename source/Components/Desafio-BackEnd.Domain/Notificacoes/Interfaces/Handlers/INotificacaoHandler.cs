using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Notificacoes.Commands;
using Desafio_BackEnd.Domain.Notificacoes.DTO;

namespace Desafio_BackEnd.Domain.Notificacoes.Interfaces.Handlers
{
    public interface INotificacaoHandler
    {
        Task<Result<NotificacaoDTO>> Handle(CreateNotificacaoCommand command);
    }
}