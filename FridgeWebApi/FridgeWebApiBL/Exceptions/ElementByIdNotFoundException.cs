using System;

namespace FridgeWebApiBL.Exceptions
{
    public class ElementByIdNotFoundException : Exception
    {
        public const string DefaultMessage = "Element not found by Id";
        public ElementByIdNotFoundException() : base(DefaultMessage) { }
        public ElementByIdNotFoundException(string message) : base(message) { }
    }
}
