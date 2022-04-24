using System;

namespace FridgeWebApiBL.Exceptions
{
    public class ElementDoesNotMatchException : Exception
    {
        public const string DefaultMessage = "Element does not match to neccessary ContentType";
        public ElementDoesNotMatchException() : base(DefaultMessage) { }
        public ElementDoesNotMatchException(string message) : base(message) { }
    }
}
