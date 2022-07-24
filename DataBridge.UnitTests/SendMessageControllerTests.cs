using DataBridge.Controllers;
using DataBridge.Dto;
using DataBridge.RabbitMQ.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQ.Client;

namespace DataBridge.UnitTests
{
    public class SendMessageControllerTests
    {
        private readonly Mock<IBroker> _broker;


        [Fact]
        public void SendMessageToBrokerTest()
        {

            //Arrange
            var message = new MessageDto() { MessageToSend = "Unit  Test" };
            var broker = new Mock<IBroker>();
            broker.Setup(c => c.SendMessage(It.IsAny<IModel>(), It.IsAny<string>()));

            var controller = new SendMessageController(broker.Object);

            //Act

            var result = controller.Post(message) as OkObjectResult;
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);


        }


    }
}