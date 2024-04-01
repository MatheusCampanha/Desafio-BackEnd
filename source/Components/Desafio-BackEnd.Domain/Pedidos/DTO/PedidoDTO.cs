﻿using Desafio_BackEnd.Domain.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio_BackEnd.Domain.Pedidos.DTO
{
    public record PedidoDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public DateTime DataCriacao { get; init; }
        public decimal Valor { get; init; }
        public string Situacao { get; init; }
        public string? EntregadorId { get; init; }

        public PedidoDTO(Pedido pedido) : this(pedido.Id, pedido.DataCriacao, pedido.Valor, pedido.Situacao, pedido.EntregadorId)
        {
        }

        public PedidoDTO(string id, DateTime dataCriacao, decimal valor, string situacao, string? entregadorId)
        {
            Id = id;
            DataCriacao = dataCriacao;
            Valor = valor;
            Situacao = situacao;
            EntregadorId = entregadorId;
        }
    }
}