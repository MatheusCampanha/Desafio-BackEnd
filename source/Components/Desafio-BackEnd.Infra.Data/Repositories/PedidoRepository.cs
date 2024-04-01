using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Motos;
using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Pedidos;
using Desafio_BackEnd.Domain.Pedidos.DTO;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<PedidoDTO> _pedidos;

        public PedidoRepository(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionStrings.DBApplication);
            var database = client.GetDatabase(settings.ConnectionStrings.DatabaseName);
            _pedidos = database.GetCollection<PedidoDTO>("Pedido");
        }

        public async Task<Result<Pedido>> GetById(string id)
        {
            var result = await _pedidos.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (result != null)
            {
                var pedido = new Pedido(result.Id, result.DataCriacao, result.Valor, result.Situacao);
                return new Result<Pedido>(HttpStatusCode.OK.GetHashCode(), pedido);
            }

            return new Result<Pedido>(HttpStatusCode.NoContent.GetHashCode(), null);
        }

        public async Task<List<PedidoDTO>> GetResult()
        {
            var filter = Builders<PedidoDTO>.Filter.Empty;

            var pedidos = await _pedidos.Find(filter).ToListAsync();

            return pedidos;
        }

        public async Task<Result<PedidoDTO>> Insert(PedidoDTO pedido)
        {
            await _pedidos.InsertOneAsync(pedido);

            return new Result<PedidoDTO>(HttpStatusCode.Created.GetHashCode(), pedido);
        }

        public async Task<CommandResult> Update(PedidoDTO pedido)
        {
            var result = await _pedidos.ReplaceOneAsync(x => x.Id == pedido.Id, pedido);

            if (result.ModifiedCount > 0)
                return new CommandResult(HttpStatusCode.NoContent.GetHashCode());
            else
                return new CommandResult(HttpStatusCode.NotFound.GetHashCode());
        }

        public async Task<CommandResult> Delete(string id)
        {
            var result = await _pedidos.DeleteOneAsync(x => x.Id.Equals(id));

            if (result.DeletedCount > 0)
                return new CommandResult(HttpStatusCode.NoContent.GetHashCode());
            else
                return new CommandResult(HttpStatusCode.NotFound.GetHashCode());
        }
    }
}