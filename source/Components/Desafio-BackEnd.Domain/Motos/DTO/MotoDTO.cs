using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio_BackEnd.Domain.Motos.DTO
{
    public record MotoDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public int Ano { get; init; }
        public string Modelo { get; init; }
        public string Placa { get; init; }
        public bool Alugada { get; init; }

        public MotoDTO(Moto moto) : this(moto.Id, moto.Ano, moto.Modelo, moto.Placa, moto.Alugada)
        {
        }

        public MotoDTO(string id, int ano, string modelo, string placa, bool alugada)
        {
            Id = id;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
            Alugada = alugada;
        }
    }
}