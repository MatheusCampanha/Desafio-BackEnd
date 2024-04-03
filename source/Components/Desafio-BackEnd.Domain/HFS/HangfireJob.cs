using Desafio_BackEnd.Domain.Pedidos.Interfaces.Handlers;

namespace Desafio_BackEnd.Domain.HFS
{
    public class HangfireJob(IPedidoEventHandler pedidoEventHandler)
    {
        private readonly IPedidoEventHandler _pedidoEventHandler = pedidoEventHandler ?? throw new ArgumentNullException(nameof(pedidoEventHandler));

        public async Task StartListening()
        {
            await _pedidoEventHandler.StartListening();
        }
    }
}