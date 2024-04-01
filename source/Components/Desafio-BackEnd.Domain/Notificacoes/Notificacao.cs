using Desafio_BackEnd.Domain.Core.Entities;

namespace Desafio_BackEnd.Domain.Notificacoes
{
    public class Notificacao : Entity
    {
        public Notificacao(string id, string pedidoId, string entregadorId, DateTime data)
        {
            SetId(id);
            SetPedidoId(pedidoId);
            SetEntregadorId(entregadorId);
            SetData(data);
        }

        public Notificacao(string pedidoId, string entregadorId, DateTime data)
        {
            SetPedidoId(pedidoId);
            SetEntregadorId(entregadorId);
            SetData(data);
        }

        public string Id { get; set; } = default!;
        public string PedidoId { get; set; } = default!;
        public string EntregadorId { get; set; } = default!;
        public DateTime Data { get; set; }

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
    }
}