using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.AppException;

namespace Application.AppException.Exceptions
{
    public class NotFoundException : HttpResponseException
    {
        private string _message;

        public NotFoundException(string message) : base(StatusCodes.Status400BadRequest)
        {
            _message = message;
        }

        public NotFoundException() : base(StatusCodes.Status400BadRequest)
        {
            _message = ExceptionConstants.NOT_FOUND;
        }

        public override string Message => _message;
    }
}