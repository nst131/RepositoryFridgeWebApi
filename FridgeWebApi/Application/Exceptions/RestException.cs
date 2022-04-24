using System;
using System.Net;

namespace Application.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string ErrorMessage { get; private set; }

        public RestException(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
