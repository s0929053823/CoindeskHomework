using NLog;
using System.Net;
using System.Text.Json;
using ILogger = NLog.ILogger;

namespace CoindeskHomework.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                throw new Exception("sadad");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An unexpected error occurred."); 
                await HandleExceptionAsync(httpContext, ex);  
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // 可以根據需要返回錯誤訊息
            var response = new { message = "An unexpected error occurred." };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
