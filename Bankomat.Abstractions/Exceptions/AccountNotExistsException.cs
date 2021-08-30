using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomat.Abstractions.Exceptions
{

    [Serializable]
    public class AccountNotExistsException : Exception
    {
        public AccountNotExistsException() { }
        public AccountNotExistsException(string message) : base(message) { }
        public AccountNotExistsException(string message, Exception inner) : base(message, inner) { }
        protected AccountNotExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
