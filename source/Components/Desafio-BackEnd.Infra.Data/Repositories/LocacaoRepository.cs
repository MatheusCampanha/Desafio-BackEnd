using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Locacoes.DTO;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Repositories;
using MongoDB.Driver;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly IMongoCollection<LocacaoDTO> _locacoes;

        public LocacaoRepository(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _locacoes = database.GetCollection<LocacaoDTO>("Locacao");
        }

        public async Task<Result<LocacaoDTO>> Insert(LocacaoDTO locacao)
        {
            await _locacoes.InsertOneAsync(locacao);

            return new Result<LocacaoDTO>(HttpStatusCode.Created.GetHashCode(), locacao);
        }
    }
}