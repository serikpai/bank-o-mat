using System;
using System.Collections.Generic;
using System.Text;

namespace DataStorage.Abstractions.Exceptions
{

    [Serializable]
    public class AccountIdMustNotBeZeroException : Exception
    {
        public AccountIdMustNotBeZeroException() { }
        public AccountIdMustNotBeZeroException(string message) : base(message) { }
        public AccountIdMustNotBeZeroException(string message, Exception inner) : base(message, inner) { }
        protected AccountIdMustNotBeZeroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
