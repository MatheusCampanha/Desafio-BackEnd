namespace Desafio_BackEnd.WebAPP.Models.Locacao
{
    public class LocacaoViewModel
    {
        public string? Id { get; set; }
        public string EntregadorId { get; set; } = default!;
        public string? MotoId { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataPrevisaoEntrega { get; set; }
        public decimal? ValorTotal { get; set; }
        public int? Plano { get; set; }
        public bool? Finalizada { get; set; }
    }
}