using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Motos;
using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using MongoDB.Driver;
using System.Net;
using System.Numerics;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly IMongoCollection<MotoDTO> _motos;

        public MotoRepository(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _motos = database.GetCollection<MotoDTO>("Moto");
        }

        public async Task<Result<Moto>> GetById(string id)
        {
            var result = await _motos.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (result != null)
            {
                var moto = new Moto(result.Id, result.Ano, result.Modelo, result.Placa, result.Alugada);
                return new Result<Moto>(HttpStatusCode.OK.GetHashCode(), moto);
            }

            return new Result<Moto>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<List<MotoDTO>> GetResult(string? placa)
        {
            var filter = Builders<MotoDTO>.Filter.Empty;

            if (!string.IsNullOrEmpty(placa))
                filter = Builders<MotoDTO>.Filter.Eq(x => x.Placa, placa);

            var motos = await _motos.Find(filter).ToListAsync();

            return motos;
        }

        public async Task<List<MotoDTO>> GetAvaiable()
        {
            var filter = Builders<MotoDTO>.Filter.Eq(x => x.Alugada, false);

            var motos = await _motos.Find(filter).ToListAsync();

            return motos;
        }

        public async Task<Result<MotoDTO>> Insert(MotoDTO moto)
        {
            await _motos.InsertOneAsync(moto);

            return new Result<MotoDTO>(HttpStatusCode.Created.GetHashCode(), moto);
        }

        public async Task<CommandResult> Update(MotoDTO moto)
        {
            var result = await _motos.ReplaceOneAsync(x => x.Id == moto.Id, moto);

            if (result.ModifiedCount > 0)
                return new CommandResult(HttpStatusCode.NoContent.GetHashCode());
            else
                return new CommandResult(HttpStatusCode.NotFound.GetHashCode());
        }

        public async Task<CommandResult> Delete(string id)
        {
            var result = await _motos.DeleteOneAsync(x => x.Id.Equals(id));

            if (result.DeletedCount > 0)
                return new CommandResult(HttpStatusCode.NoContent.GetHashCode());
            else
                return new CommandResult(HttpStatusCode.NotFound.GetHashCode());
        }
    }
}