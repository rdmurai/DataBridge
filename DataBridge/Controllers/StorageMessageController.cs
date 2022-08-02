using DataBridge.Domain.Entities;
using DataBridge.Mongo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DataBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageMessageController : ControllerBase
    {
        private readonly IStorageMessage _storageMessage;
        public StorageMessageController(IStorageMessage storageMessage)
        {
            _storageMessage = storageMessage;

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "manager")]
        [SwaggerOperation("Retrieve a message from Database by Id")]
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
        [SwaggerOperation("Retrieve all message from Database")]
        public async Task<IActionResult> GetAll()
        {
            var msg = await _storageMessage.GetAllAsync();

            if (msg == null)
            {
                return NotFound();
            }

            return Ok(msg);
        }


        [HttpPut]
        [SwaggerOperation("Update a message from Database")]
        public async Task<IActionResult> Put([FromBody] StorageMessage message)
        {
            await _storageMessage.UpdateAsync(message);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation("Delete a message from Database by Id")]
        public async Task<IActionResult> Delete(string id)
        {
    
            await _storageMessage.RemoveAsync(id);

            return Ok();
        }

       

    }
}

