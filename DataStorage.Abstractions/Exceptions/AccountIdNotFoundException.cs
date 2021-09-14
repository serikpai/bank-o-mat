using System;

namespace DataStorage.Abstractions.Exceptions
{
    [Serializable]
    public class AccountIdNotFoundException : Exception
    {
        public AccountIdNotFoundException() { }
        public AccountIdNotFoundException(string message) : base(message) { }
        public AccountIdNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected AccountIdNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
