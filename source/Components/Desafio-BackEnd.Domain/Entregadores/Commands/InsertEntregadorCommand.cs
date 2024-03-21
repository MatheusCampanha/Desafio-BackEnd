using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Desafio_BackEnd.Domain.Entregadores.Commands
{
    public class InsertEntregadorCommand(string nome, string cnpj, DateTime dataNascimento, string numeroCNH, TipoCNHEnum tipoCNH, IFormFile ImagemCNH) : Command
    {
        public string Nome { get; private set; } = nome;
        public string CNPJ { get; private set; } = cnpj;
        public DateTime DataNascimento { get; private set; } = dataNascimento;
        public string NumeroCNH { get; private set; } = numeroCNH;
        public TipoCNHEnum TipoCNH { get; private set; } = tipoCNH;
        public IFormFile ImagemCNH { get; private set; } = ImagemCNH;

        public override bool IsValid()
        {
            return Valid;
        }
    }
}