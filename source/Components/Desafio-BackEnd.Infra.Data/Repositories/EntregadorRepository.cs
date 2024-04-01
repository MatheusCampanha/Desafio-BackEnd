using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Locacoes.DTO;
using Desafio_BackEnd.Domain.Pedidos.DTO;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Net;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly Settings _settings;
        private readonly IMongoCollection<EntregadorDTO> _entregadores;

        public EntregadorRepository(Settings settings)
        {
            _settings = settings;

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

        public async Task<List<EntregadorDTO>> GetAll()
        {
            var entregadores = await _entregadores.Find(_ => true).ToListAsync();

            return entregadores;
        }

        public async Task<List<string>> GetAvaiable()
        {
            var locacaoRepository = new LocacaoRepository(_settings);
            var locacoesAtivas = await locacaoRepository.GetAllActives();
            var entregadoresComlocacoes = locacoesAtivas.Select(x => x.EntregadorId).ToList();

            var client = new MongoClient(_settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(_settings.ConnectionStrings.DatabaseName);
            var pedidos = database.GetCollection<PedidoDTO>("Pedido");

            var filter = Builders<PedidoDTO>.Filter.In(p => p.EntregadorId, entregadoresComlocacoes) &
                         Builders<PedidoDTO>.Filter.Eq(p => p.Situacao, "Aceito");

            var pedidosAceitos = pedidos.Find(filter).ToList();
            var entregadoresAceitos = new HashSet<string>(pedidosAceitos.Select(p => p.EntregadorId));

            return entregadoresComlocacoes.Where(id => !entregadoresAceitos.Contains(id)).ToList();
        }

        public async Task<EntregadorDTO> GetByIdResult(string id)
        {
            var result = await _entregadores.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            return result;
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

        public string SaveImagemCNH(string numeroCNH, IFormFile imagemFile)
        {
            try
            {
                var directory = $"entregador_{numeroCNH}";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var filePath = Path.Combine(directory, imagemFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagemFile.CopyTo(stream);
                }

                return filePath;
            }
            catch (Exception)
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

        public void ReplaceImage(string filePath, IFormFile newImageFile)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Arquivo não encontrado", filePath);

                using var stream = new FileStream(filePath, FileMode.Create);
                newImageFile.CopyTo(stream);
            }
            catch (Exception) { }
        }

        public bool DeleteImage(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}