using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Domain.Notificacoes;
using Desafio_BackEnd.Domain.Notificacoes.DTO;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Pedidos.Event;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Handlers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Desafio_BackEnd.Domain.Pedidos.Handlers
{
    public class PedidoEventHandler(INotificacaoRepository notificacaoRepository, Settings settings) : IPedidoEventHandler
    {
        private readonly INotificacaoRepository _notificacaoRepository = notificacaoRepository;
        private readonly Settings _settings = settings;

        public async Task StartListening()
        {
            await Task.Run(() =>
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

                channel.QueueDeclare(queue: _settings.RabbitMQConfigurations.RoutingKey, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var pedidoEvent = JsonConvert.DeserializeObject<PedidoEvent>(json);

                    await Handle(pedidoEvent);
                };

                channel.BasicConsume(queue: _settings.RabbitMQConfigurations.RoutingKey, autoAck: true, consumer: consumer);
            });
        }

        public async Task Handle(PedidoEvent @event)
        {
            var notificacao = new Notificacao(@event.PedidoId, @event.EntregadorId, @event.Data, @event.Valor, false);

            var notificacaoDTO = new NotificacaoDTO(notificacao);
            await _notificacaoRepository.Insert(notificacaoDTO);
        }
    }
}