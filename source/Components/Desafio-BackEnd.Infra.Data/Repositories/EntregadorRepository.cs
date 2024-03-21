using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Infra.Data.Helpers;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly IMongoCollection<EntregadorDTO> _entregadores;
        private readonly Settings _settings;
        private readonly IS3Helper _s3Helper;

        public EntregadorRepository(Settings settings, IS3Helper s3Helper)
        {
            _settings = settings;
            _s3Helper = s3Helper;

            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _entregadores = database.GetCollection<EntregadorDTO>("Entregador");
        }

        public async Task<Result<Entregador>> GetById(string id)
        {
            var result = await _entregadores.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (result != null)
            {
                var entregador = new Entregador(result.Nome, result.CNPJ, result.DataNascimento, result.NumeroCNH, result.TipoCNH, result.CaminhoImagemCNH);
                return new Result<Entregador>(HttpStatusCode.OK.GetHashCode(), entregador);
            }

            return new Result<Entregador>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<EntregadorDTO>> GetByIdResult(string id)
        {
            var entregador = await _entregadores.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (entregador != null)
                return new Result<EntregadorDTO>(HttpStatusCode.OK.GetHashCode(), entregador);
            else
                return new Result<EntregadorDTO>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<EntregadorDTO>> GetByCNPJResult(string cnpj)
        {
            var entregador = await _entregadores.Find(x => x.CNPJ == cnpj).FirstOrDefaultAsync();

            if (entregador != null)
                return new Result<EntregadorDTO>(HttpStatusCode.OK.GetHashCode(), entregador);
            else
                return new Result<EntregadorDTO>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<EntregadorDTO>> GetByNumeroCNHResult(string numeroCNH)
        {
            var entregador = await _entregadores.Find(x => x.NumeroCNH == numeroCNH).FirstOrDefaultAsync();

            if (entregador != null)
                return new Result<EntregadorDTO>(HttpStatusCode.OK.GetHashCode(), entregador);
            else
                return new Result<EntregadorDTO>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<Result<EntregadorDTO>> Insert(EntregadorDTO entregador)
        {
            await _entregadores.InsertOneAsync(entregador);

            return new Result<EntregadorDTO>(HttpStatusCode.Created.GetHashCode(), entregador);
        }

        public async Task<CommandResult> Update(EntregadorDTO entregador)
        {
            var result = await _entregadores.ReplaceOneAsync(x => x.Id == entregador.Id, entregador);

            if (result.ModifiedCount > 0)
                return new CommandResult(HttpStatusCode.NoContent.GetHashCode());
            else
                return new CommandResult(HttpStatusCode.NotFound.GetHashCode());
        }

        public async Task<CommandResult> Delete(string id)
        {
            var result = await _entregadores.DeleteOneAsync(x => x.Id.Equals(id));

            if (result.DeletedCount > 0)
                return new CommandResult(HttpStatusCode.NoContent.GetHashCode());
            else
                return new CommandResult(HttpStatusCode.NotFound.GetHashCode());
        }

        public async Task<string> UploadImagemCNH(IFormFile imagemCNH)
        {
            var contentType = "image/png";
            using var memoryStream = new MemoryStream();
            await imagemCNH.CopyToAsync(memoryStream);
            var imgPath = await _s3Helper.Upload(_settings.S3Settings.BucketName, _settings.S3Settings.Key, memoryStream, contentType);
            return imgPath;
        }

        public async Task DeleteImagemCNH(string caminhoImagemCNH)
        {
            await _s3Helper.Delete(_settings.S3Settings.BucketName, caminhoImagemCNH);
        }

        public async Task<byte[]> DownloadImagemCNH(string caminhoImagemCNH)
        {
            using var memoryStream = new MemoryStream();
            await _s3Helper.Download(_settings.S3Settings.BucketName, caminhoImagemCNH, memoryStream);
            memoryStream.Position = 0;
            return memoryStream.ToArray();
        }
    }
}