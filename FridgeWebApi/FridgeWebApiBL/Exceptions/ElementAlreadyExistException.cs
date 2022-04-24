using System;

namespace FridgeWebApiBL.Exceptions
{
    public class ElementAlreadyExistException : Exception
    {
        public const string DefaultMessage = "Element already exist";
        public ElementAlreadyExistException() : base(DefaultMessage) { }
        public ElementAlreadyExistException(string message) : base(message) { }
    }
}
