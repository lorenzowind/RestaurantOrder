using FluentValidation.Results;
using MediatR;
using RestaurantOrder.API.Commands;
using RestaurantOrder.API.Core.Application;
using RestaurantOrder.API.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantOrder.API.CommandHandlers
{
    public class OrderCommandHandler : CommandHandler, 
        IRequestHandler<ValidateOrderCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(ValidateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return Task.FromResult(request.ValidationResult);

            bool isDishTypeValid = RestaurantMenu.IsDishTypeValid(request.DishType);

            if (!isDishTypeValid)
            {
                AddError($"Dish type '{request.DishType}' is not valid.");
                return Task.FromResult(ValidationResult);
            }

            request.Items.Distinct().ToList().ForEach(item =>
            {
                if (!RestaurantMenu.IsAllowedItem(request.DishType, item))
                    AddError($"Order item '{item}' is not allowed to dish type '{request.DishType}'.");

                else if (RestaurantMenu.GetItemRepetitions(item, request.Items) > 1 && 
                    !RestaurantMenu.CanItemBeRepeated(request.DishType, item))
                        AddError($"Order item '{item}' can not be repeated to dish type '{request.DishType}'.");
            });

            return Task.FromResult(ValidationResult);
        }
    }
}
