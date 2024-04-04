using Desafio_BackEnd.Domain.Core.Commands;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Pedidos.Commands
{
    public class UpdateSituacaoPedidoCommand(string situacao, string entregadorId) : Command
    {
        [JsonIgnore]
        public string? Id { get; private set; }

        public string Situacao { get; private set; } = situacao;
        public string EntregadorId { get; private set; } = entregadorId;

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