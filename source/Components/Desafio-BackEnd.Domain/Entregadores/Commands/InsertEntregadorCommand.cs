using Desafio_BackEnd.Domain.Core.Commands;

namespace Desafio_BackEnd.Domain.Entregadores.Commands
{
    public class InsertEntregadorCommand(string nome, string cNPJ, DateTime dataNascimento, string numeroCNH, string tipoCNH) : Command
    {
        public string Nome { get; private set; } = nome;
        public string CNPJ { get; private set; } = cNPJ;
        public DateTime DataNascimento { get; private set; } = dataNascimento;
        public string NumeroCNH { get; private set; } = numeroCNH;
        public string TipoCNH { get; private set; } = tipoCNH;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}