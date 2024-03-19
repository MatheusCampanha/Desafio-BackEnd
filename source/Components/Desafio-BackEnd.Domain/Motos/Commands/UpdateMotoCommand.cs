using Desafio_BackEnd.Domain.Core.Commands;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Motos.Commands
{
    public class UpdateMotoCommand(int ano, string modelo, string placa) : Command
    {
        [JsonIgnore]
        public string? Id { get; private set; }

        public int Ano { get; private set; } = ano;
        public string Modelo { get; private set; } = modelo;
        public string Placa { get; private set; } = placa;

        public override bool IsValid()
        {
            if (Ano <= 0)
                AddNotification(nameof(Ano), "Deve ser maior que zero");

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