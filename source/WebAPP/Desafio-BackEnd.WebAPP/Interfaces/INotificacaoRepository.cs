using Desafio_BackEnd.WebAPP.Models.Notificacao;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface INotificacaoRepository
    {
        Task<List<Notificacao>> Get(string token, string? entregadorId);
    }
}