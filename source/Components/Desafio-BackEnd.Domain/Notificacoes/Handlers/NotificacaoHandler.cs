using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Notificacoes.Commands;
using Desafio_BackEnd.Domain.Notificacoes.DTO;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories;
using System.Net;

namespace Desafio_BackEnd.Domain.Notificacoes.Handlers
{
    public class NotificacaoHandler(INotificacaoRepository notificacaoRepository, IPedidoRepository pedidoRepository, IEntregadorRepository entregadorRepository) : INotificacaoHandler
    {
        private readonly INotificacaoRepository _notificacaoRepository = notificacaoRepository;
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        public async Task<Result<NotificacaoDTO>> Handle(CreateNotificacaoCommand command)
        {
            var errorResult = new Result<NotificacaoDTO>(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var pedidoResult = await _pedidoRepository.GetById(command.PedidoId);
            if (pedidoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(pedidoResult);
                return errorResult;
            }

            var entregadorResult = await _entregadorRepository.GetById(command.EntregadorId);
            if (entregadorResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(entregadorResult); ;
                return errorResult;
            }

            var notificacao = new Notificacao(command.PedidoId, command.EntregadorId, command.Data);

            if (notificacao.Invalid)
            {
                errorResult.AddNotifications(notificacao);
                return errorResult;
            }

            var notificacaoDTO = new NotificacaoDTO(notificacao);
            return await _notificacaoRepository.Create(notificacaoDTO);
        }
    }
}