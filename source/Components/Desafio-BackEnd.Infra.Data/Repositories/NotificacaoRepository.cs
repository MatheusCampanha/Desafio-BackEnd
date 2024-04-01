using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Notificacoes.DTO;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories;
using MongoDB.Driver;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly IMongoCollection<NotificacaoDTO> _notificacoes;

        public NotificacaoRepository(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _notificacoes = database.GetCollection<NotificacaoDTO>("Notificacao");
        }

        public async Task<List<NotificacaoDTO>> GetAll()
        {
            var result = await _notificacoes.Find(_ => true).ToListAsync();

            return result;
        }

        public async Task<Result<NotificacaoDTO>> Create(NotificacaoDTO notificao)
        {
            await _notificacoes.InsertOneAsync(notificao);

            return new Result<NotificacaoDTO>(HttpStatusCode.Created.GetHashCode(), notificao);
        }
    }
}