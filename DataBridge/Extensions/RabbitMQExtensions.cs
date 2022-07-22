using RabbitMQ.Client;

namespace DataBridge.Extensions
{
    public static class RabbitMQExtensions
    {

        public static void AddRabbitMQ(this IServiceCollection services)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            connection.CreateModel();
        }
    }
}
