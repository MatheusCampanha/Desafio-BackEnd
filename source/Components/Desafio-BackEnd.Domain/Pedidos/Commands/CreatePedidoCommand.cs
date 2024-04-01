using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
