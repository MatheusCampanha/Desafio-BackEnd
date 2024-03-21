using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Entregadores.Commands
{
    public class UpdateEntregadorCommand(string nome, string cnpj, DateTime dataNascimento, string numeroCNH, TipoCNHEnum tipoCNH, IFormFile imagemCNH) : Command
    {
        [JsonIgnore]
        public string? Id { get; private set; }

        public string Nome { get; private set; } = nome;
        public string CNPJ { get; private set; } = cnpj;
        public DateTime DataNascimento { get; private set; } = dataNascimento;
        public string NumeroCNH { get; private set; } = numeroCNH;
        public TipoCNHEnum TipoCNH { get; private set; } = tipoCNH;
        public IFormFile ImagemCNH { get; private set; } = imagemCNH;

        public override bool IsValid()
        {
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