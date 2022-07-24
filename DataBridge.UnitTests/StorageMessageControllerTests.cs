using DataBridge.Controllers;
using DataBridge.Domain.Entities;
using DataBridge.Mongo;
using DataBridge.Mongo.Interfaces;
using DataBridge.Mongo.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;

namespace DataBridge.UnitTests
{
    public class StorageMessageControllerTests
    {
        private StorageMessageRepository _storageMessageRepository;
        private readonly IOptions<MessageDbSettings> _options = Options.Create(new MessageDbSettings()
        {
            ConnectionString = "mongodb://root:example@localhost:27017",
            DatabaseName = "MessageDb",
            CollectionName = "StorageMessage"
        });

        public StorageMessageControllerTests()
        {
            _storageMessageRepository = new StorageMessageRepository(_options);
        }

        [Fact]
        public async Task Get_When_Found_Message_ById()

        {
            //Arrange
            var message = new StorageMessage() { Id = "62dae6051c04796fd0fe3856", Message = "test1", DateTime = DateTime.Now };

            var _storageMessage = new Mock<IStorageMessage>();
            _storageMessage.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(message);

            var controller = new StorageMessageController(_storageMessage.Object);

            //Act
            var data = await controller.Get(message.Id);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Get_When_NotFound_Message_ById()

        {
            //Arrange
            var message = new StorageMessage() { Id = "62dae6051c04796fd0fe3856", Message = "test1", DateTime = DateTime.Now };
            var _storageMessage = new Mock<IStorageMessage>();
            _storageMessage.Setup(c => c.GetAsync(It.IsAny<string>()));

            var controller = new StorageMessageController(_storageMessage.Object);

            //Act
            var data = await controller.Get(message.Id) as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMessage_WhenMessageExists()

        {
            //Arrange
            var messages = new List<StorageMessage>();
            messages.Add(new StorageMessage() { Id = "62dae6051c04796fd0fe3856", Message = "test1" });
            messages.Add(new StorageMessage() { Id = "62dae6051c04796fd0fe3857", Message = "test2" });
            messages.Add(new StorageMessage() { Id = "62dae6051c04796fd0fe3858", Message = "test3" });

            var _storageMessage = new Mock<IStorageMessage>();
            _storageMessage.Setup(c => c.GetAllAsync()).ReturnsAsync(messages);
            var controller = new StorageMessageController(_storageMessage.Object);
            
            //Act
            var items = await controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(items);
        }

        [Fact]
        public async Task Delete_Message_WhenMessageExists()

        {
            //Arrange
            var message = new StorageMessage() { Id = "62dae6051c04796fd0fe3856", Message = "test1", DateTime = DateTime.Now };

            var _storageMessage = new Mock<IStorageMessage>();
            _storageMessage.Setup(c => c.RemoveAsync(It.IsAny<string>()));
            var controller = new StorageMessageController(_storageMessage.Object);
            //Act
            var data = await controller.Delete(message.Id);
            
            //Assert
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async Task Put_Message_WhenMessageExists()

        {
            //Arrange
            var message = new StorageMessage() { Id = "62dae6051c04796fd0fe3856", Message = "test2", DateTime = DateTime.Now };

            var _storageMessage = new Mock<IStorageMessage>();
            _storageMessage.Setup(c => c.UpdateAsync(It.IsAny<StorageMessage>()));
            var controller = new StorageMessageController(_storageMessage.Object);
            //Act
            var data = await controller.Put(message);

            //Assert
            Assert.IsType<OkResult>(data);
        }
    }
}
