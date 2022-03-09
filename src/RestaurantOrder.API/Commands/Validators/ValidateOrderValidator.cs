using FluentValidation;

namespace RestaurantOrder.API.Commands.Validators
{
    public class ValidateOrderValidator : AbstractValidator<ValidateOrderCommand>
    {
        public ValidateOrderValidator()
        {
            RuleFor(o => o.DishType)
                .NotEmpty()
                .WithMessage("Dish type is required.");

            RuleFor(c => c.Items.Count)
                .GreaterThan(0)
                .WithMessage("Order must have 1 item or more");
        }
    }
}
