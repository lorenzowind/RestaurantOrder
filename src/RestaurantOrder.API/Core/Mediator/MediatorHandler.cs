using FluentValidation.Results;
using MediatR;
using RestaurantOrder.API.Core.Application;
using System.Threading.Tasks;

namespace RestaurantOrder.API.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> SendCommand<T>(T cmnd) where T : Command
        {
            return await _mediator.Send(cmnd);
        }
    }
}
