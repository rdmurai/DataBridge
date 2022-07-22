using DataBridge.Broker.Settings;
using DataBridge.Domain.Entities;
using DataBridge.Mongo.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DataBridge.Services
{
    public class RabbitMqService : BackgroundService
    {
        private readonly IOptions<BrokerSettings> _settings;
        private readonly IStorageMessage _storageMessage;

        public RabbitMqService(IOptions<BrokerSettings> brokerSettings, IStorageMessage storageMessage)
        {
            _settings = brokerSettings;
            _storageMessage = storageMessage;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _connectionFactory = new ConnectionFactory();
            _connectionFactory.UserName = _settings.Value.Username;
            _connectionFactory.Password = _settings.Value.Password;
            _connectionFactory.VirtualHost = _settings.Value.VirtualHost;
            _connectionFactory.HostName = _settings.Value.HostName;
            _connectionFactory.Port = _settings.Value.Port;
            var _connection = _connectionFactory.CreateConnection();
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: _settings.Value.QueueName,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                if (message != null && message != "")
                {
                    var msg = new StorageMessage() { DateTime = DateTime.Now, Message = message };
                    await _storageMessage.CreateAsync(msg);
                }
            };
            channel.BasicConsume(queue: _settings.Value.QueueName,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
