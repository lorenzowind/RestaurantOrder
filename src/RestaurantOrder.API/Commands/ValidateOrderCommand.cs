using RestaurantOrder.API.Commands.Validators;
using RestaurantOrder.API.Core.Application;
using System.Collections.Generic;

namespace RestaurantOrder.API.Commands
{
    public class ValidateOrderCommand : Command
    {
        public string DishType { get; set; }
        public List<int> Items { get; set; }

        public ValidateOrderCommand(string dishType, List<int> items)
        { 
            DishType = dishType;
            Items = items;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidateOrderValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
