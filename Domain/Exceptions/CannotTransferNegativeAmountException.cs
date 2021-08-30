using System;

namespace Domain.Exceptions
{
    [Serializable]
    public class CannotTransferNegativeAmountException : Exception
    {
        public CannotTransferNegativeAmountException() { }
        public CannotTransferNegativeAmountException(string message) : base(message) { }
        public CannotTransferNegativeAmountException(string message, Exception inner) : base(message, inner) { }
        protected CannotTransferNegativeAmountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
