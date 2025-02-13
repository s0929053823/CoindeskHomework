using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace CoindeskHomework.Filters
{
    public class ApiLoggingFilter : IAsyncActionFilter
    {
        private readonly ApplicationDbContext _dbContext;


        public ApiLoggingFilter(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            var requestBody = await ReadRequestBodyAsync(request);
            var requestHeaders = JsonSerializer.Serialize(request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));

            var stopwatch = Stopwatch.StartNew(); 
            var executedContext = await next();  
            stopwatch.Stop();                     

            var response = executedContext.HttpContext.Response;
            var responseBody = await ReadResponseBodyAsync(response);
            var responseHeaders = JsonSerializer.Serialize(response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));

            var log = new ApiLog
            {
                RequestPath = request.Path,
                Method = request.Method,
                RequestBody = requestBody,
                ResponseBody = responseBody,
                StatusCode = response.StatusCode,
                RequestHeaders = requestHeaders,
                ResponseHeaders = responseHeaders,
                Timestamp = DateTime.UtcNow,
                ExecutionSeconds = stopwatch.Elapsed.Seconds
            };

            _dbContext.ApiLogs.Add(log);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            if (request.ContentLength == null || request.ContentLength == 0)
                return string.Empty;

            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; 
            return body;
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            if (response.Body == null || !response.Body.CanRead)
                return string.Empty;

            response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(response.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }
}
