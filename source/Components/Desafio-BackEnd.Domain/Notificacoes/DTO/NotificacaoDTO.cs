using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio_BackEnd.Domain.Notificacoes.DTO
{
    public class NotificacaoDTO(string id, string pedidoId, string entregadorId, DateTime data, decimal valor, bool lida)
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = id;

        public string PedidoId { get; set; } = pedidoId;
        public string EntregadorId { get; set; } = entregadorId;
        public DateTime Data { get; set; } = data;
        public decimal Valor { get; set; } = valor;
        public bool Lida { get; set; } = lida;

        public NotificacaoDTO(Notificacao notificacao) : this(notificacao.Id, notificacao.PedidoId, notificacao.EntregadorId, notificacao.Data, notificacao.Valor, notificacao.Lida)
        {
        }
    }
}