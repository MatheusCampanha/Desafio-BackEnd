using Desafio_BackEnd.Domain.Core.Entities;

namespace Desafio_BackEnd.Domain.Locacoes
{
    public class Locacao : Entity
    {
        public Locacao(string? id, string entregadorId, string motoId, DateTime dataInicial, DateTime dataFinal, DateTime dataPrevisaoEntrega, decimal valorTotal)
        {
            SetId(id);
            SetEntregadorId(entregadorId);
            SetMotoId(motoId);
            SetDataInicial(dataInicial);
            SetDataFinal(dataFinal);
            SetDataPrevisaoEntrega(dataPrevisaoEntrega);
            SetValorTotal(valorTotal);
        }

        public Locacao(string entregadorId, string motoId, DateTime dataInicial, DateTime dataFinal, DateTime dataPrevisaoEntrega, decimal valorTotal)
        {
            SetEntregadorId(entregadorId);
            SetMotoId(motoId);
            SetDataInicial(dataInicial);
            SetDataFinal(dataFinal);
            SetDataPrevisaoEntrega(dataPrevisaoEntrega);
            SetValorTotal(valorTotal);
        }

        public string? Id { get; private set; }
        public string EntregadorId { get; set; } = default!;
        public string MotoId { get; set; } = default!;
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime DataPrevisaoEntrega { get; set; }
        public decimal ValorTotal { get; set; }

        public void SetId(string? id)
        {
            if (Valid)
                Id = id;
        }

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
    }
}