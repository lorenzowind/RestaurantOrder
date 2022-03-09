using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestaurantOrder.API.CommandHandlers;
using RestaurantOrder.API.Commands;
using RestaurantOrder.API.Core.Mediator;

namespace RestaurantOrder.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<ValidateOrderCommand, ValidationResult>, OrderCommandHandler>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
