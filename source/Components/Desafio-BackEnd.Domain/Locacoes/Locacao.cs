using Desafio_BackEnd.Domain.Core.Entities;

namespace Desafio_BackEnd.Domain.Locacoes
{
    public class Locacao : Entity
    {
        public Locacao(string entregadorId, string motoId, DateTime dataInicial, DateTime dataFinal, DateTime dataPrevisaoEntrega, decimal valorTotal, int plano, bool finalizada)
        {
            SetEntregadorId(entregadorId);
            SetMotoId(motoId);
            SetDataInicial(dataInicial);
            SetDataFinal(dataFinal);
            SetDataPrevisaoEntrega(dataPrevisaoEntrega);
            SetValorTotal(valorTotal);
            SetPlano(plano);
            SetFinalizada(finalizada);
        }

        public string? Id { get; private set; }
        public string EntregadorId { get; private set; } = default!;
        public string MotoId { get; private set; } = default!;
        public DateTime DataInicial { get; private set; }
        public DateTime DataFinal { get; private set; }
        public DateTime DataPrevisaoEntrega { get; private set; }
        public decimal ValorTotal { get; private set; }
        public int Plano { get; private set; }
        public bool Finalizada { get; private set; }

        public void SetEntregadorId(string entregadorId)
        {
            if (Valid)
                EntregadorId = entregadorId;
        }

        public void SetMotoId(string motoId)
        {
            if (Valid)
                MotoId = motoId;
        }

        public void SetDataInicial(DateTime dataInicial)
        {
            if (Valid)
                DataInicial = dataInicial;
        }

        public void SetDataFinal(DateTime dataFinal)
        {
            if (Valid)
                DataFinal = dataFinal;
        }

        public void SetDataPrevisaoEntrega(DateTime dataPrevisaoEntrega)
        {
            if (Valid)
                DataPrevisaoEntrega = dataPrevisaoEntrega;
        }

        public void SetValorTotal(decimal valorTotal)
        {
            if (Valid)
                ValorTotal = valorTotal;
        }

        public void SetPlano(int plano)
        {
            if (Valid)
                Plano = plano;
        }

        public void SetFinalizada(bool finalizada)
        {
            if (Valid)
                Finalizada = finalizada;
        }
    }
}