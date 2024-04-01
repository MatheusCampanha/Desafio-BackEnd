using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_BackEnd.Domain.Notificacoes.DTO
{
    public class NotificacaoDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        public string PedidoId { get; set; } = default!;
        public string EntregadorId { get; set; } = default!;
        public DateTime Data { get; set; }

        public NotificacaoDTO(Notificacao notificacao) : this(notificacao.Id, notificacao.PedidoId, notificacao.EntregadorId, notificacao.Data)
        {

        }

        public NotificacaoDTO(string id, string pedidoId, string entregadorId, DateTime data)
        {
            Id = id;
            PedidoId = pedidoId;
            EntregadorId = entregadorId;
            Data = data;
        }
    }
}
