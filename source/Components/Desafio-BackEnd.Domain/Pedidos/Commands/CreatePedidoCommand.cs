using Desafio_BackEnd.Domain.Core.Commands;

namespace Desafio_BackEnd.Domain.Pedidos.Commands
{
    public class CreatePedidoCommand(DateTime dataCriacao, decimal valor) : Command
    {
        public DateTime DataCriacao { get; private set; } = dataCriacao;
        public decimal Valor { get; private set; } = valor;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}