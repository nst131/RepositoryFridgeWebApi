using System;

namespace FridgeWebApiBL.Exceptions
{
    public class ElementCannotLoadException : Exception
    {
        public const string DefaultMessage = "Element cannot load";
        public ElementCannotLoadException() : base(DefaultMessage) { }
        public ElementCannotLoadException(string message) : base(message) { }
    }
}
