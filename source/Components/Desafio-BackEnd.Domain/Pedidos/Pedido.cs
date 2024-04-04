using Desafio_BackEnd.Domain.Core.Entities;

namespace Desafio_BackEnd.Domain.Pedidos
{
    public class Pedido : Entity
    {
        public Pedido(string id, DateTime dataCriacao, decimal valor, string situacao, string? entregadorId = null)
        {
            SetId(id);
            SetDataCriacao(dataCriacao);
            SetValor(valor);
            SetSituacao(situacao);
            SetEntregadorId(entregadorId);
        }

        public Pedido(DateTime dataCriacao, decimal valor, string situacao, string? entregadorId = null)
        {
            SetDataCriacao(dataCriacao);
            SetValor(valor);
            SetSituacao(situacao);
            SetSituacao(situacao);
            SetEntregadorId(entregadorId);
        }

        public string Id { get; private set; } = default!;
        public DateTime DataCriacao { get; private set; }
        public decimal Valor { get; private set; }
        public string Situacao { get; private set; } = default!;
        public string? EntregadorId { get; private set; }

        public void SetId(string id)
        {
            if (Valid)
                Id = id;
        }

        public void SetDataCriacao(DateTime dataCriacao)
        {
            if (Valid)
                DataCriacao = dataCriacao;
        }

        public void SetValor(decimal valor)
        {
            if (Valid)
                Valor = valor;
        }

        public void SetSituacao(string situacao)
        {
            if (!situacao.Equals("Disponível") && !situacao.Equals("Aceito") && !situacao.Equals("Entregue"))
                AddNotification("Pedido.situacao", "Inválida");

            if (Valid)
                Situacao = situacao;
        }

        public void SetEntregadorId(string? entregadorId)
        {
            if (Valid)
                EntregadorId = entregadorId;
        }
    }
}