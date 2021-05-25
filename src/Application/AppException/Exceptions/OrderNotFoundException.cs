using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.AppException;

namespace Application.AppException.Exceptions
{
    public class OrderNotFoundException : HttpResponseException
    {
        private string _message;

        public OrderNotFoundException(string message) : base(StatusCodes.Status400BadRequest)
        {
            _message = message;
        }

        public OrderNotFoundException() : base(StatusCodes.Status400BadRequest)
        {
            _message = ExceptionConstants.ORDER_NOT_FOUND;
        }

        public override string Message => _message;
    }
}