using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Pedidos;
using Desafio_BackEnd.Domain.Pedidos.DTO;
using Desafio_BackEnd.Domain.Pedidos.Event;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net;
using System.Text;

namespace Desafio_BackEnd.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<PedidoDTO> _pedidos;
        private readonly Settings _settings;

        public PedidoRepository(Settings settings)
        {
            _settings = settings;

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
            var pedidos = await _pedidos.Find(_ => true).ToListAsync();

            return pedidos;
        }

        public async Task<List<PedidoDTO>> GetUnfinished()
        {
            var pedidos = await _pedidos.Find(x => !x.Situacao.Equals("Entregue")).ToListAsync();

            return pedidos;
        }

        public async Task<Result<PedidoDTO>> Insert(PedidoDTO pedido)
        {
            try
            {
                await _pedidos.InsertOneAsync(pedido);

                var entregadorRepository = new EntregadorRepository(_settings);
                var entregadoresDisponiveis = await entregadorRepository.GetAvaiable();

                foreach (var entregador in entregadoresDisponiveis)
                {
                    var @event = new PedidoEvent(pedido.Id, entregador, pedido.DataCriacao, pedido.Valor);
                    SendMessage(@event);
                }

                return new Result<PedidoDTO>(HttpStatusCode.Created.GetHashCode(), pedido);
            }
            catch (Exception)
            {
                await Delete(pedido.Id);
                throw;
            }
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

        private void SendMessage(PedidoEvent @event)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_settings.RabbitMQConfigurations.Url),
                Port = _settings.RabbitMQConfigurations.Port,
                UserName = _settings.RabbitMQConfigurations.UserName,
                Password = _settings.RabbitMQConfigurations.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(_settings.RabbitMQConfigurations.RoutingKey, false, false, false, null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _settings.RabbitMQConfigurations.RoutingKey,
                        basicProperties: null,
                        body: body);
        }
    }
}