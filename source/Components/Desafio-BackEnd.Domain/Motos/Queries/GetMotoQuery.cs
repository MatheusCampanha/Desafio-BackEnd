using Desafio_BackEnd.Domain.Core.Queries;

namespace Desafio_BackEnd.Domain.Motos.Queries
{
    public class GetMotoQuery : Query
    {
        public string? Placa { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}