using DataBridge.Broker.Settings;
using DataBridge.RabbitMQ.Interface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DataBridge.Broker
{
    public class RabbitMQBroker : IBroker
    {
        private readonly IOptions<BrokerSettings> _settings;
        public RabbitMQBroker(IOptions<BrokerSettings> brokerSettings)
        {
            _settings = brokerSettings;
        }
        public IModel ConnectToBroker()
        {
            var _connectionFactory = new ConnectionFactory();
            _connectionFactory.UserName = _settings.Value.Username;
            _connectionFactory.Password = _settings.Value.Password;
            _connectionFactory.VirtualHost = _settings.Value.VirtualHost;
            _connectionFactory.HostName = _settings.Value.HostName;
            _connectionFactory.Port = _settings.Value.Port;
            var _connection = _connectionFactory.CreateConnection();
            var channel = _connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            return channel;
        }

        public void SendMessage(IModel channel, string message)
        {
            channel.QueueDeclare(queue: _settings.Value.QueueName,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: _settings.Value.QueueName,
                                 basicProperties: null,
                                 body: body);
        }

        public string ReceiveMessage(IModel channel)
        {
            channel.QueueDeclare(queue: _settings.Value.QueueName,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);


            var message = "";
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: _settings.Value.QueueName,
                                 autoAck: true,
                                 consumer: consumer);

            return message;

        }
    }
}
