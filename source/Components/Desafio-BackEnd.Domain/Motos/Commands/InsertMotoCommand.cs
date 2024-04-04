using Desafio_BackEnd.Domain.Core.Commands;

namespace Desafio_BackEnd.Domain.Motos.Commands
{
    public class InsertMotoCommand(int ano, string modelo, string placa) : Command
    {
        public int Ano { get; private set; } = ano;
        public string Modelo { get; private set; } = modelo;
        public string Placa { get; private set; } = placa;

        public override bool IsValid()
        {
            if (Ano <= 0)
                AddNotification(nameof(Ano), "Deve ser maior que zero");

            return Valid;
        }
    }
}