namespace Desafio_BackEnd.Domain.Pedidos.Event
{
    public class PedidoEvent(string pedidoId, string entregadorId, DateTime data, decimal valor)
    {
        public string PedidoId { get; private set; } = pedidoId;
        public string EntregadorId { get; private set; } = entregadorId;
        public DateTime Data { get; private set; } = data;
        public decimal Valor { get; private set; } = valor;
    }
}