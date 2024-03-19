using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio_BackEnd.Domain.Moto.DTO
{
    public record MotoDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public int Ano { get; init; }
        public string Modelo { get; init; }
        public string Placa { get; init; }

        public MotoDTO(Moto moto) : this(moto.Id?.ToString(), moto.Ano, moto.Modelo, moto.Placa)
        {
        }

        public MotoDTO(string id, int ano, string modelo, string placa)
        {
            Id = id;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }
    }
}