using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.AppException;

namespace Application.AppException.Exceptions
{
    public class UserBlockedException : HttpResponseException
    {
        private string _message;

        public UserBlockedException(string message) : base(StatusCodes.Status406NotAcceptable)
        {
            _message = message;
        }

        public UserBlockedException() : base(StatusCodes.Status406NotAcceptable)
        {
            _message = ExceptionConstants.USER_BLOCKED;
        }

        public override string Message => _message;
    }
}