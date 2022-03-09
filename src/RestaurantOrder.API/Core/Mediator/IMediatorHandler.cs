using FluentValidation.Results;
using RestaurantOrder.API.Core.Application;
using System.Threading.Tasks;

namespace RestaurantOrder.API.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> SendCommand<T>(T cmnd) where T : Command;
    }
}
