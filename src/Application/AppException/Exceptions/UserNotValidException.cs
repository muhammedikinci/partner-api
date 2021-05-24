using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.AppException;

namespace Application.AppException.Exceptions
{
    public class UserNotValidException : HttpResponseException
    {
        private string _message;

        public UserNotValidException(string message) : base(StatusCodes.Status400BadRequest)
        {
            _message = message;
        }

        public UserNotValidException() : base(StatusCodes.Status400BadRequest)
        {
            _message = ExceptionConstants.USER_IS_NOT_VALID;
        }

        public override string Message => _message;
    }
}