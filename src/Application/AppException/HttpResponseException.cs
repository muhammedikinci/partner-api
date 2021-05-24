using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.AppException
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = StatusCodes.Status500InternalServerError;

        public HttpResponseException(int statusCode)
        {
            Status = statusCode;
        }
    }
}