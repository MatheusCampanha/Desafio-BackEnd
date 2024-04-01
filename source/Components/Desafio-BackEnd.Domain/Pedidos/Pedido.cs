using Desafio_BackEnd.Domain.Core.Entities;
using Desafio_BackEnd.Domain.Core.Enums;

namespace Desafio_BackEnd.Domain.Pedidos
{
    public class Pedido : Entity
    {
        public string Id { get; set; } = default!;
        public DateTime DataCriacao { get; set; }
        public decimal Valor { get; set; }
        public SituacaoPedidoEnum Situacao { get; set; }

        public void DefinirId(string id)
        {
            if (Valid)
                Id = id;
        }

        public void DefinirDataCriacao(DateTime dataCriacao)
        {
            if (Valid)
                DataCriacao = dataCriacao;
        }
    }
}