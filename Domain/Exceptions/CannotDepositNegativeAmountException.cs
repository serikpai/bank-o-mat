using System;

namespace Domain.Exceptions
{

    [Serializable]
    public class CannotDepositNegativeAmountException : Exception
    {
        public CannotDepositNegativeAmountException() { }
        public CannotDepositNegativeAmountException(string message) : base(message) { }
        public CannotDepositNegativeAmountException(string message, Exception inner) : base(message, inner) { }
        protected CannotDepositNegativeAmountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
