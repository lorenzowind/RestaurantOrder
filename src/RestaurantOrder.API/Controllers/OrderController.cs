using Microsoft.AspNetCore.Mvc;
using RestaurantOrder.API.Commands;
using RestaurantOrder.API.Core.Application;
using RestaurantOrder.API.Core.Mediator;
using RestaurantOrder.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantOrder.API.Controllers
{
    public class OrderController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public OrderController(IMediatorHandler mediatorHandler)
        {
            _mediator = mediatorHandler;
        }


        [HttpPost("validate-order")]
        public async Task<IActionResult> ValidateOrder(string order)
        {
            if (string.IsNullOrEmpty(order))
            {
                AddProcessingError("Order is not well-formed.");
                return CustomResponse();
            }

            string dishType;
            List<int> items = new();

            try
            {
                string[] orderInput = order.Split(", ");

                dishType = orderInput[0];

                for (int i = 1; i < orderInput.Length; i++) items.Add(int.Parse(orderInput[i]));
            }
            catch
            {
                AddProcessingError("Order items are not well-formed.");
                return CustomResponse();
            }

            var response = await _mediator.SendCommand(new ValidateOrderCommand(dishType, items));

            foreach (var error in response.Errors) AddProcessingError(error.ErrorMessage);

            return CustomResponse(new Order(dishType, items).GenerateOutput());
        }
    }
}
