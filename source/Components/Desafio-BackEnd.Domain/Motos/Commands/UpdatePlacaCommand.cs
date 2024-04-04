using Desafio_BackEnd.Domain.Core.Commands;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Motos.Commands
{
    public class UpdatePlacaCommand(string placa) : Command
    {
        [JsonIgnore]
        public string? Id { get; private set; }

        public string Placa { get; private set; } = placa;

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Placa))
                AddNotification(nameof(Placa), "Não deve ser vazia");

            return Valid;
        }

        public void AlterId(string id)
        {
            if (Invalid)
                return;

            Id = id;
        }
    }
}