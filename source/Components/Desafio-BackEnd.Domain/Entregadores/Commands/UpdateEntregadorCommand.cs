using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Entregadores.Commands
{
    public class UpdateEntregadorCommand(string imagemBase64, string nomeArquivo) : Command
    {
        [JsonIgnore]
        public string? Id { get; private set; }

        public string ImagemBase64 { get; private set; } = imagemBase64;
        public string NomeArquivo { get; private set; } = nomeArquivo;

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