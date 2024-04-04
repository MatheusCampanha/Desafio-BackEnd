namespace Desafio_BackEnd.WebAPP.Models.Notificacao
{
    public class NotificacaoViewModel
    {
        public List<Notificacao> Notificacoes { get; set; } = default!;
        public bool IsUserAdmin { get; set; }
    }

    public class Notificacao
    {
        public string Id { get; set; } = default!;
        public string PedidoId { get; set; } = default!;
        public string EntregadorId { get; set; } = default!;
        public string? EntregadorNome { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public bool Lida { get; set; }
    }
}