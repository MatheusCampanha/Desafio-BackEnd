using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio_BackEnd.Domain.Entregadores.DTO
{
    public record EntregadorDTO
    {
        public EntregadorDTO(string id, string nome, string cnpj, DateTime dataNascimento, string numeroCNH, string tipoCNH, string? caminhoImagemCNH)
        {
            Id = id;
            Nome = nome;
            CNPJ = cnpj;
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH;
            TipoCNH = tipoCNH;
            CaminhoImagemCNH = caminhoImagemCNH;
        }

        public EntregadorDTO(Entregador entregador) : this(entregador.Id, entregador.Nome, entregador.CNPJ, entregador.DataNascimento, entregador.NumeroCNH, entregador.TipoCNH, entregador.CaminhoImagemCNH)
        {
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public string Nome { get; init; }
        public string CNPJ { get; init; }
        public DateTime DataNascimento { get; init; }
        public string NumeroCNH { get; init; }
        public string TipoCNH { get; init; }
        public string? CaminhoImagemCNH { get; init; }
    }
}