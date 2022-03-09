using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestaurantOrder.API.Core.Messages;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.API.Core.Application
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            if (result != null)
            {
                return BadRequest(new object[] 
                {
                    new Dictionary<string, object> 
                    { 
                        { "Output", result } 
                    }, 
                    new ValidationProblemDetails(new Dictionary<string, string[]>
                    {
                        { "Messages", Errors.ToArray() }
                    })
                });
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddProcessingError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddProcessingError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult response)
        {
            HasResponseErrors(response);

            return CustomResponse();
        }

        protected bool HasResponseErrors(ResponseResult response)
        {
            if (response == null || !response.Errors.Messages.Any()) return false;

            foreach (var message in response.Errors.Messages)
            {
                AddProcessingError(message);
            }

            return true;
        }

        protected bool IsValidOperation() => !Errors.Any();

        protected void AddProcessingError(string error) => Errors.Add(error);

        protected void ClearProcessingErrors() => Errors.Clear();
    }
}
