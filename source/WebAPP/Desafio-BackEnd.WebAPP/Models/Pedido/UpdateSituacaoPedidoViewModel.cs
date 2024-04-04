namespace Desafio_BackEnd.WebAPP.Models.Pedido
{
    public class UpdateSituacaoPedidoViewModel
    {
        public string PedidoId { get; set; } = default!;
        public string Situacao { get; set; } = default!;
        public string EntregadorId { get; set; } = default!;
    }
}
