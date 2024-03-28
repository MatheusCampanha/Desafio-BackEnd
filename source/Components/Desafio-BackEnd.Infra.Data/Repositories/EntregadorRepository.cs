using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using MongoDB.Driver;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly IMongoCollection<EntregadorDTO> _entregadores;

        public EntregadorRepository(Settings settings)
        {
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

        public string SaveImagemCNH(string numeroCNH, string imagemBase64, string nomeArquivo)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(imagemBase64);

                var directory = $"entregador_{numeroCNH}";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var filePath = Path.Combine(directory, nomeArquivo);

                File.WriteAllBytes(filePath, bytes);

                return filePath;
            }
            catch (ApplicationException)
            {
                return string.Empty;
            }
        }

        public byte[] GetImagemCNH(string caminhoImagemCNH)
        {
            try
            {
                if (!File.Exists(caminhoImagemCNH))
                    throw new FileNotFoundException("Arquivo não encontrado", caminhoImagemCNH);

                return File.ReadAllBytes(caminhoImagemCNH);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void ReplaceImage(string filePath, byte[] newImageBytes)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Arquivo não encontrado", filePath);

                File.WriteAllBytes(filePath, newImageBytes);
            }
            catch (Exception) { }
        }
    }
}