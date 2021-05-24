using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.AppException
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(new {ErrorMessage = context.Exception.Message})
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is Exception)
            {
                var message = context.Exception.InnerException != null ? context.Exception.InnerException.Message : context.Exception.Message;

                context.Result = new ObjectResult(new {ErrorMessage = context.Exception.Message})
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}