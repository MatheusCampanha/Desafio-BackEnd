using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.Domain.Notificacoes.DTO;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories;
using MongoDB.Driver;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly IMongoCollection<NotificacaoDTO> _notificacoes;
        private readonly Settings _settings;

        public NotificacaoRepository(Settings settings)
        {
            _settings = settings; ;

            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _notificacoes = database.GetCollection<NotificacaoDTO>("Notificacao");
        }

        public async Task<List<NotificacaoDTO>> Get(string? entregadorId)
        {
            var filter = Builders<NotificacaoDTO>.Filter.Empty;

            if (!string.IsNullOrEmpty(entregadorId))
                filter = Builders<NotificacaoDTO>.Filter.Eq(x => x.EntregadorId, entregadorId);

            var result = await _notificacoes.Find(filter).ToListAsync();

            var entregadorRepository = new EntregadorRepository(_settings);

            if (!string.IsNullOrEmpty(entregadorId))
            {
                var entregadorResult = await entregadorRepository.GetById(entregadorId);
                var entregador = entregadorResult.QueryResult.Registros.First();

                foreach (var notificacao in result)
                    notificacao.EntregadorNome = entregador.Nome;
            }
            else
            {
                var entregadores = await entregadorRepository.GetAll();
                var entregadoresDictionary = entregadores.ToDictionary(entregador => entregador.Id, entregador => entregador.Nome);

                foreach (var notificacao in result)
                {
                    if (entregadoresDictionary.TryGetValue(notificacao.EntregadorId, out var nomeEntregador))
                        notificacao.EntregadorNome = nomeEntregador;
                }
            }

            return result;
        }

        public async Task<Result<NotificacaoDTO>> Create(NotificacaoDTO notificao)
        {
            await _notificacoes.InsertOneAsync(notificao);

            return new Result<NotificacaoDTO>(HttpStatusCode.Created.GetHashCode(), notificao);
        }
    }
}