﻿using Desafio_BackEnd.Domain.Core.Commands;
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
                var entregador = new Entregador(result.Id, result.UserId, result.Nome, result.CNPJ, result.DataNascimento, result.NumeroCNH, result.TipoCNH, result.CaminhoImagemCNH);
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
            var entregadores = await _entregadores.Find(x => x.TipoCNH.Equals("A")).ToListAsync();

            if (entregadores.Count == 0)
                return [];

            var locacaoRepository = new LocacaoRepository(_settings);
            var locacoesAtivas = await locacaoRepository.GetAllActives();

            var entregadoresComLocacao = new List<string>();
            foreach (var entregador in entregadores)
            {
                if (locacoesAtivas.Any(l => l.EntregadorId == entregador.Id))
                    entregadoresComLocacao.Add(entregador.Id);
            }

            if (entregadoresComLocacao.Count == 0)
                return [];

            var pedidoRepository = new PedidoRepository(_settings);
            var pedidos = await pedidoRepository.GetUnfinished();

            if (pedidos.Count == 0)
                return [];

            var entregadoresDisponiveis = entregadoresComLocacao
                .Where(e => !pedidos.Any(p => !string.IsNullOrEmpty(p.EntregadorId) && p.EntregadorId.Equals(e)))
                .ToList();

            return entregadoresDisponiveis;
        }

        public async Task<EntregadorDTO> GetByIdResult(string id)
        {
            var result = await _entregadores.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            return result;
        }

        public async Task<EntregadorDTO> GetByUserIdResult(string userId)
        {
            var result = await _entregadores.Find(x => x.UserId.Equals(userId)).FirstOrDefaultAsync();

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
            var updateDefinition = Builders<EntregadorDTO>.Update
                .Set(x => x.CaminhoImagemCNH, entregador.CaminhoImagemCNH);

            var result = await _entregadores.UpdateOneAsync(
                x => x.Id == entregador.Id,
                updateDefinition
            );

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
    }
}