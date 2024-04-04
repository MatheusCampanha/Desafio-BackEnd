using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Pedidos.Commands;
using Desafio_BackEnd.Domain.Pedidos.DTO;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories;
using System.Net;

namespace Desafio_BackEnd.Domain.Pedidos.Handlers
{
    public class PedidoHandler(IPedidoRepository pedidoRepository, IEntregadorRepository entregadorRepository) : IPedidoHandler
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;

        public async Task<Result<PedidoDTO>> Handle(CreatePedidoCommand command)
        {
            var errorResult = new Result<PedidoDTO>(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var pedido = new Pedido(command.DataCriacao, command.Valor, "Disponível");

            if (pedido.Invalid)
            {
                errorResult.AddNotifications(pedido);
                return errorResult;
            }

            var pedidoDTO = new PedidoDTO(pedido);
            return await _pedidoRepository.Insert(pedidoDTO);
        }

        public async Task<CommandResult> Handle(UpdateSituacaoPedidoCommand command)
        {
            var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var pedidoResult = await _pedidoRepository.GetById(command.Id!);
            if (pedidoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(pedidoResult);
                return errorResult;
            }
            var pedido = pedidoResult.QueryResult.Registros.First();

            var entregadorResult = await _entregadorRepository.GetById(command.EntregadorId);
            if (entregadorResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(entregadorResult);
                return errorResult;
            }

            pedido.SetSituacao(command.Situacao);
            pedido.SetEntregadorId(command.EntregadorId);

            if (pedido.Invalid)
            {
                errorResult.AddNotifications(pedido);
                return errorResult;
            }

            var pedidoDTO = new PedidoDTO(pedido);
            return await _pedidoRepository.Update(pedidoDTO);
        }

        public async Task<CommandResult> Handle(string id)
        {
            var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

            var pedidoResult = await _pedidoRepository.GetById(id);
            if (pedidoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(pedidoResult);
                return errorResult;
            }

            return await _pedidoRepository.Delete(id);
        }
    }
}