using Desafio_BackEnd.Domain.Core.Commands;

namespace Desafio_BackEnd.Domain.Notificacoes.Commands
{
    public class CreateNotificacaoCommand(string pedidoId, string entregadorId, DateTime data) : Command
    {
        public string PedidoId { get; private set; } = pedidoId;
        public string EntregadorId { get; private set; } = entregadorId;
        public DateTime Data { get; private set; } = data;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}