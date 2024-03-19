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

        public QueryResult(int paginaAtual, int registrosPorPagina, long totalRegistros, ICollection<T> registros)
        {
            PaginaAtual = paginaAtual;
            RegistrosPorPagina = registrosPorPagina;
            TotalRegistros = totalRegistros;
            Registros = registros;
        }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        public int PaginaAtual { get; private set; }
        public int RegistrosPorPagina { get; private set; }
        public long TotalRegistros { get; private set; }
        public ICollection<T> Registros { get; private set; }
    }
}