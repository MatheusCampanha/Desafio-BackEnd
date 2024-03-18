using Desafio_BackEnd.Domain.Core.Notifications;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Core.Commands
{
    public class CommandResult : Notifiable
    {
        public CommandResult(int statusCode)
        {
            StatusCode = statusCode;
        }

        public CommandResult(int statusCode, Notifiable item)
        {
            StatusCode = statusCode;
            AddNotifications(item);
        }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }

    public class CommandResult<T>(int statusCode, int paginaAtual, int registrosPorPagina, long totalRegistros, ICollection<T> registros) : Notifiable
    {
        [JsonIgnore]
        public int StatusCode { get; set; } = statusCode;

        public int PaginaAtual { get; set; } = paginaAtual;
        public int RegistrosPorPagina { get; set; } = registrosPorPagina;
        public long TotalRegistros { get; set; } = totalRegistros;
        public ICollection<T> Registros { get; set; } = registros;
    }
}