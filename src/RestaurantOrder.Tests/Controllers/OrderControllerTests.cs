using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantOrder.API.Commands;
using RestaurantOrder.API.Controllers;
using RestaurantOrder.API.Core.Mediator;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrder.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly OrderController _orderController;
        private readonly Mock<IMediatorHandler> _mediator;

        public OrderControllerTests()
        {
            _mediator = new Mock<IMediatorHandler>();
            _orderController = new OrderController(_mediator.Object);
        }

        [Fact]
        public async Task Validate_Order_Endpoint_Should_Succeed_When_Request_Body_Is_Filled()
        {
            // Act
            _mediator.Setup(m => m.SendCommand(It.IsAny<ValidateOrderCommand>())).ReturnsAsync(new ValidationResult());
            string order = "morning, 1, 2, 3";

            // Arrange
            OkObjectResult result = (OkObjectResult) await _orderController.ValidateOrder(order);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Validate_Order_Endpoint_Should_Fails_When_Request_Body_Is_Not_Filled()
        {
            // Act
            string order = string.Empty;

            // Arrange
            BadRequestObjectResult result = (BadRequestObjectResult) await _orderController.ValidateOrder(order);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
