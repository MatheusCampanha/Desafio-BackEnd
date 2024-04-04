using Desafio_BackEnd.Domain.Core.Notifications;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Core.Queries
{
    public class QueryResult<T> : Notifiable where T : class
    {
        [JsonIgnore]
        public readonly string _dominio = typeof(T).Name.ToString().Replace("QueryResult", "");

        public QueryResult(T registro)
        {
            PaginaAtual = 1;
            Registros = [registro];

            RegistrosPorPagina = Registros.Count;
            TotalRegistros = Registros.Count;
        }

        public int PaginaAtual { get; private set; }
        public int RegistrosPorPagina { get; private set; }
        public long TotalRegistros { get; private set; }
        public ICollection<T> Registros { get; private set; }
    }
}