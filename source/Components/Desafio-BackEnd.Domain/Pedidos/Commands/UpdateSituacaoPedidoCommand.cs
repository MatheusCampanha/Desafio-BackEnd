using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Desafio_BackEnd.Domain.Pedidos.Commands
{
    public class UpdateSituacaoPedidoCommand(string situacao, string entregadorId) : Command
    {
        [JsonIgnore]
        public string? Id { get; set; }
        public string Situacao { get; set; } = situacao;
        public string EntregadorId { get; set; } = entregadorId;

        public override bool IsValid()
        {
            if (!Situacao.Equals("Disponível") && !Situacao.Equals("Aceito") && !Situacao.Equals("Entregue"))
                AddNotification(nameof(Situacao), "Inválida");

            return Valid;
        }

        public void SetId(string id)
        {
            if (Valid)
                Id = id;
        }
    }
}