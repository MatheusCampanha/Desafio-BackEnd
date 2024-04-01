namespace Desafio_BackEnd.WebAPP.Models.Pedido
{
    public class PedidoViewModel
    {
        public DateTime DataCriacao { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; } = default!;
    }
}
