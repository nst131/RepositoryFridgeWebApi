using System;

namespace FridgeWebApiBL.Exceptions
{
    public class ElementOutOfRangeException : Exception
    {
        public const string DefaultMessage = "Element Out Of Range";
        public ElementOutOfRangeException() : base(DefaultMessage) { }
        public ElementOutOfRangeException(string message) : base(message) { }
    }
}
