using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.AppException;

namespace Application.AppException.Exceptions
{
    public class PasswordException : HttpResponseException
    {
        private string _message;

        public PasswordException(string message) : base(StatusCodes.Status400BadRequest)
        {
            _message = message;
        }

        public PasswordException() : base(StatusCodes.Status400BadRequest)
        {
            _message = "Şifre alanı boş bırakılamaz!";
        }

        public override string Message => _message;
    }
}