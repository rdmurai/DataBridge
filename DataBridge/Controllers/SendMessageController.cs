using DataBridge.Domain.Entities;
using DataBridge.Dto;
using DataBridge.Mongo.Interfaces;
using DataBridge.RabbitMQ.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DataBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendMessageController : ControllerBase
    {
        private readonly IBroker _broker;
        private readonly IStorageMessage _storageMessage;
        public SendMessageController(IBroker broker, IStorageMessage storageMessage)
        {
            _broker = broker;
            _storageMessage = storageMessage;

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var msg = await _storageMessage.GetAsync(id);
            if (msg == null)
            {
                return NotFound();
            }

            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var msg = await _storageMessage.GetAllAsync();

            if (msg == null)
            {
                return NotFound();
            }

            return Ok(msg);
        }


        [HttpPost]
        public IActionResult Post([FromBody] MessageDto message)
        {
            string msg = JsonSerializer.Serialize(message);

            _broker.SendMessage(_broker.ConnectToBroker(), msg);

            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] StorageMessage message)
        {
            await _storageMessage.UpdateAsync(message);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _storageMessage.RemoveAsync(id);

            return Ok();
        }

    }
}

