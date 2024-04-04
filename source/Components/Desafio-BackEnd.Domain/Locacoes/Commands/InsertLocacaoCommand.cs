using Desafio_BackEnd.Domain.Core.Commands;

namespace Desafio_BackEnd.Domain.Locacoes.Commands
{
    public class InsertLocacaoCommand(string entregadorId, string motoId, DateTime dataInicial, DateTime dataFinal, DateTime dataPrevisaoEntrega, decimal valorTotal, int plano, bool finalizada) : Command
    {
        public string EntregadorId { get; private set; } = entregadorId;
        public string MotoId { get; private set; } = motoId;
        public DateTime DataInicial { get; private set; } = dataInicial;
        public DateTime DataFinal { get; private set; } = dataFinal;
        public DateTime DataPrevisaoEntrega { get; private set; } = dataPrevisaoEntrega;
        public decimal ValorTotal { get; private set; } = valorTotal;
        public int Plano { get; private set; } = plano;
        public bool Finalizada { get; private set; } = finalizada;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}