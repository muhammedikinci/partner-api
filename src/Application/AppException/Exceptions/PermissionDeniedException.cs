using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.AppException;

namespace Application.AppException.Exceptions
{
    public class PermissionDeniedException : HttpResponseException
    {
        private string _message;

        public PermissionDeniedException(string message) : base(StatusCodes.Status400BadRequest)
        {
            _message = message;
        }

        public PermissionDeniedException() : base(StatusCodes.Status400BadRequest)
        {
            _message = ExceptionConstants.PERMISSON_DENIED;
        }

        public override string Message => _message;
    }
}