using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio_BackEnd.Domain.Locacoes.DTO
{
    public record LocacaoDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        public string EntregadorId { get; init; }
        public string MotoId { get; init; }
        public DateTime DataInicial { get; init; }
        public DateTime DataFinal { get; init; }
        public DateTime DataPrevisaoEntrega { get; init; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal ValorTotal { get; init; }
        public int Plano { get; init; }
        public bool  Finalizada { get; init; }

        public LocacaoDTO(Locacao locacao) : this(locacao.Id, locacao.EntregadorId, locacao.MotoId, locacao.DataInicial, locacao.DataFinal, locacao.DataPrevisaoEntrega, locacao.ValorTotal, locacao.Plano, locacao.Finalizada)
        {
        }

        public LocacaoDTO(string id, string entregadorId, string motoId, DateTime dataInicial, DateTime dataFinal, DateTime dataPrevisaoEntrega, decimal valorTotal, int plano, bool finalizada)
        {
            Id = id;
            EntregadorId = entregadorId;
            MotoId = motoId;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            DataPrevisaoEntrega = dataPrevisaoEntrega;
            ValorTotal = valorTotal;
            Plano = plano;
            Finalizada = finalizada;
        }
    }
}