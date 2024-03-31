using Desafio_BackEnd.Domain.Core.Commands;

namespace Desafio_BackEnd.Domain.Locacoes.Commands
{
    public class InsertLocacaoCommand(string entregadorId, string motoId, DateTime dataInicial, DateTime dataFinal, DateTime dataPrevisaoEntrega, decimal valorTotal) : Command
    {
        public string EntregadorId { get; set; } = entregadorId;
        public string MotoId { get; set; } = motoId;
        public DateTime DataInicial { get; set; } = dataInicial;
        public DateTime DataFinal { get; set; } = dataFinal;
        public DateTime DataPrevisaoEntrega { get; set; } = dataPrevisaoEntrega;
        public decimal ValorTotal { get; set; } = valorTotal;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}