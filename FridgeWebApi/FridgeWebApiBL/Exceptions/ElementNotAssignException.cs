using System;

namespace FridgeWebApiBL.Exceptions
{
    public class ElementNotAssignException : Exception
    {
        public const string DefaultMessage = "Element was not assigned";
        public ElementNotAssignException() : base(DefaultMessage) { }
        public ElementNotAssignException(string message) : base(message) { }
    }
}
