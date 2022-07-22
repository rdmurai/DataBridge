using DataBridge.Broker.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DataBridge.RabbitMQ.Interface
{
    public interface IBroker
    {
        /// <summary>
        /// Connect to a Message Broker
        /// </summary>
        /// <returns></returns>
        IModel ConnectToBroker();

        /// <summary>
        /// Send Message to a Broker
        /// </summary>
        /// <param name="channel">RabbitMQ Channel</param>
        /// <param name="message">Message to be send</param>
        /// <returns></returns>
        void SendMessage(IModel channel, string message);

        /// <summary>
        /// Receive Message from Broker
        /// </summary>
        /// <param name="channel"></param>
        string ReceiveMessage(IModel channel);
    }
}