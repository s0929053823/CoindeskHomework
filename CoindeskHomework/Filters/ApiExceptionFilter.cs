using CoindeskHomework.Data;
using CoindeskHomework.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CoindeskHomework.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ApplicationDbContext  _dbContext;

        public ApiExceptionFilter(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;   
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var request = context.HttpContext.Request;

            var log = new ApiLog
            {
                Method = request.Method,
                RequestPath = request.Path,
                StatusCode = StatusCodes.Status500InternalServerError,
                ExceptionMessage = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            _dbContext.ApiLogs.Add(log);
            _dbContext.SaveChanges();

          
            var errorResponse = new
            {
                message = "An unexpected error occurred.",
                error = exception.Message
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
