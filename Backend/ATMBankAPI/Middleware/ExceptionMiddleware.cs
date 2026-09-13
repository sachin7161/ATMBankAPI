using System.Net;
using System.Text.Json;

namespace ATMBankAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode = (int)HttpStatusCode.InternalServerError;

            if (exception.Message.Contains("not authorized",
                StringComparison.OrdinalIgnoreCase))
            {
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            else if (exception.Message.Contains("Not Found",
                StringComparison.OrdinalIgnoreCase))
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception.Message.Contains("Insufficent",
                StringComparison.OrdinalIgnoreCase) ||
                     exception.Message.Contains("cannot",
                     StringComparison.OrdinalIgnoreCase) ||
                     exception.Message.Contains("blocked",
                     StringComparison.OrdinalIgnoreCase))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception.Message.Contains("Insufficent",
    StringComparison.OrdinalIgnoreCase) ||
         exception.Message.Contains("cannot",
         StringComparison.OrdinalIgnoreCase) ||
         exception.Message.Contains("blocked",
         StringComparison.OrdinalIgnoreCase) ||
         exception.Message.Contains("Invalid Refresh Token",
         StringComparison.OrdinalIgnoreCase) ||
         exception.Message.Contains("expired",
         StringComparison.OrdinalIgnoreCase) ||
         exception.Message.Contains("revoked",
         StringComparison.OrdinalIgnoreCase))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                statusCode = statusCode,
                message = exception.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}