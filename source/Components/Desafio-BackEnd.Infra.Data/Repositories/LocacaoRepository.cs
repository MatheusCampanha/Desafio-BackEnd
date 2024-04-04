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

        public async Task EndRate(string id)
        {
            var filter = Builders<LocacaoDTO>.Filter.Eq(x => x.Id, id);
            var update = Builders<LocacaoDTO>.Update.Set(x => x.Finalizada, true);

            await _locacoes.UpdateOneAsync(filter, update);
        }

        public async Task<LocacaoDTO> IsActive(string id)
        {
            var result = await _locacoes.Find(x => x.EntregadorId.Equals(id) && !x.Finalizada).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<LocacaoDTO>> GetAllActives()
        {
            var result = await _locacoes.Find(x => !x.Finalizada).ToListAsync();

            return result;
        }

        public async Task<Result<LocacaoDTO>> Insert(LocacaoDTO locacao)
        {
            await _locacoes.InsertOneAsync(locacao);

            return new Result<LocacaoDTO>(HttpStatusCode.Created.GetHashCode(), locacao);
        }
    }
}