using Desafio_BackEnd.Domain.Core.Queries;

namespace Desafio_BackEnd.Domain.Motos.Queries
{
    public class GetMotoQuery : Query
    {
        public string? Id { get; set; }
        public int? Ano { get; set; }
        public string? Modelo { get; set; }
        public string? Placa { get; set; }

        public override bool IsValid()
        {
            if (Ano <= 0)
                AddNotification(nameof(Ano), "Deve ser maior que zero");

            return base.IsValid();
        }
    }
}