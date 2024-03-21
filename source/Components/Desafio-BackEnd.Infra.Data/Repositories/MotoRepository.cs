using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Motos;
using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Motos.Queries;
using MongoDB.Driver;
using System.Net;

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
                var moto = new Moto(result.Id, result.Ano, result.Modelo, result.Placa);
                return new Result<Moto>(HttpStatusCode.OK.GetHashCode(), moto);
            }

            return new Result<Moto>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<MotoDTO>> GetByIdResult(string id)
        {
            var moto = await _motos.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (moto != null)
                return new Result<MotoDTO>(HttpStatusCode.OK.GetHashCode(), moto);
            else
                return new Result<MotoDTO>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<MotoDTO>> GetResult(string placa)
        {
            var moto = await _motos.Find(x => x.Placa == placa).FirstOrDefaultAsync();

            if (moto != null)
                return new Result<MotoDTO>(HttpStatusCode.OK.GetHashCode(), moto);
            else
                return new Result<MotoDTO>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<MotoDTO>> GetResult(GetMotoQuery query)
        {
            var filterBuilder = Builders<MotoDTO>.Filter;

            var filter = filterBuilder.Empty; // Filtro inicial vazio

            // Construa o filtro com base nos parâmetros de consulta
            if (!string.IsNullOrEmpty(query.Placa))
                filter &= filterBuilder.Eq(x => x.Placa, query.Placa);

            var totalRegistros = await _motos.CountDocumentsAsync(filter);

            var skip = query.RegistrosPorPagina * (query.PaginaAtual - 1);
            var motos = await _motos.Find(filter).Skip(skip).Limit(query.RegistrosPorPagina).ToListAsync();

            int statusCode;
            if (motos.Count > 0)
                statusCode = HttpStatusCode.PartialContent.GetHashCode();
            else
                statusCode = HttpStatusCode.UnprocessableEntity.GetHashCode();

            var result = new Result<MotoDTO>(statusCode, query.PaginaAtual, query.RegistrosPorPagina, totalRegistros, motos);

            return result;
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