using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBridge.Broker.Settings
{
    public class BrokerSettings
    {
        public string QueueName { get; set; } = null!;

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string VirtualHost { get; set; } = null!;
        public string HostName { get; set; } = null!;
        public int Port { get; set; } 
    }
}
