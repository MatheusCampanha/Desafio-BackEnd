namespace Desafio_BackEnd.WebAPP.Models.Pedido
{
    public class PedidoViewModel
    {
        public List<Pedido> Pedidos { get; set; } = default!;
        public bool IsUserAdmin { get; set; }
    }

    public class Pedido
    {
        public string Id { get; set; } = default!;
        public DateTime DataCriacao { get; set; }
        public string? EntregadorId { get; set; }
        public string? EntregadorNome { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; } = default!;
    }
}
