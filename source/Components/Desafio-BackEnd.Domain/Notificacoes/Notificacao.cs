using Desafio_BackEnd.Domain.Core.Entities;

namespace Desafio_BackEnd.Domain.Notificacoes
{
    public class Notificacao : Entity
    {
        public Notificacao(string id, string pedidoId, string entregadorId, DateTime data, decimal valor, bool lida)
        {
            SetId(id);
            SetPedidoId(pedidoId);
            SetEntregadorId(entregadorId);
            SetData(data);
            SetValor(valor);
            SetLida(lida);
        }

        public Notificacao(string pedidoId, string entregadorId, DateTime data, decimal valor, bool lida)
        {
            SetPedidoId(pedidoId);
            SetEntregadorId(entregadorId);
            SetData(data);
            SetValor(valor);
            SetLida(lida);
        }

        public string Id { get; private set; } = default!;
        public string PedidoId { get; private set; } = default!;
        public string EntregadorId { get; private set; } = default!;
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public bool Lida { get; private set; }

        public void SetId(string id)
        {
            if (Valid)
                Id = id;
        }

        public void SetPedidoId(string pedidoId)
        {
            if (Valid)
                PedidoId = pedidoId;
        }

        public void SetEntregadorId(string entregadorId)
        {
            if (Valid)
                EntregadorId = entregadorId;
        }

        public void SetData(DateTime data)
        {
            if (Valid)
                Data = data;
        }

        public void SetValor(decimal valor)
        {
            if (Valid)
                Valor = valor;
        }

        public void SetLida(bool lida)
        {
            if (Valid)
                Lida = lida;
        }
    }
}