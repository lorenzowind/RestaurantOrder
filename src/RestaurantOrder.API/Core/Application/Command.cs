using FluentValidation.Results;
using MediatR;
using RestaurantOrder.API.Core.Messages;
using System;

namespace RestaurantOrder.API.Core.Application
{
    public class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
