using DataBridge.Dto;
using DataBridge.RabbitMQ.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace DataBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SendMessageController : ControllerBase
    {
        private readonly IBroker _broker;

        public SendMessageController(IBroker broker)
        {
            _broker = broker;
        }

       
        [HttpPost]
        [SwaggerOperation("Send a message to Broker")]
        public IActionResult Post([FromBody] MessageDto message)
        {
            string msg = JsonSerializer.Serialize(message);

            _broker.SendMessage(_broker.ConnectToBroker(), msg);

            return Ok(msg);
        }
    }
}
