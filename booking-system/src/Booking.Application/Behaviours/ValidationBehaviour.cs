using Booking.Application.Exceptions;
using Booking.Application.Models.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booking.Application.Behaviours
{
    public  class ValidationBehaviour : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(E => E.Errors.Count > 0)
                          .SelectMany(E => E.Errors)
                          .Select(E => E.ErrorMessage)
                          .ToList();

                context.Result = new BadRequestObjectResult(new CoreException(ExceptionCodes.ValidationError, "Occured validation error", errors));
            }

            base.OnActionExecuting(context);
        }
    }
}
