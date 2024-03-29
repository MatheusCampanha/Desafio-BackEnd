namespace Desafio_BackEnd.WebAPP.Models.Entregador
{
    public class CreateEntregadorViewModel
    {
        public string Nome { get; set; } = default!;
        public string Cnpj { get; set; } = default!;
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; } = default!;
        public string TipoCNH { get; set; } = default!;
    }
}
