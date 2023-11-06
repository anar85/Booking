using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Booking.Application.Exceptions;
using Booking.Application.Models.Constants;

namespace Booking.API.Middlewares
{
    public class ExceptionHandler : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger) => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            int statusCode;
            var result = new CoreException(ExceptionCodes.UnexpectedError, "Occured unexcepted error. Please contact with system admin!");

            if (e is CustomException)
            {
                var ex = e as CustomException;
                result = new CoreException(ex.Code, ex.Message, ex.Errors);
            }

            if (e is BadRequestException)
                statusCode = StatusCodes.Status400BadRequest;
            else if (e is NotFoundException)
                statusCode = StatusCodes.Status404NotFound;
            else
            {
                _logger.LogError(CreateMessage(context, e), e);
                statusCode = StatusCodes.Status500InternalServerError;
            }

            var response = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private static string CreateMessage(HttpContext context, Exception e)
        {
            var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

            if (e.InnerException != null)
                message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";

            return $"{message} RequestId: {context.TraceIdentifier}";
        }
    }
}
