using System;

namespace Domain.Exceptions
{
    [Serializable]
    public class CannotWithdrawNegativeAmountException : Exception
    {
        public CannotWithdrawNegativeAmountException() { }
        public CannotWithdrawNegativeAmountException(string message) : base(message) { }
        public CannotWithdrawNegativeAmountException(string message, Exception inner) : base(message, inner) { }
        protected CannotWithdrawNegativeAmountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() { }
        public InsufficientBalanceException(string message) : base(message) { }
        public InsufficientBalanceException(string message, Exception inner) : base(message, inner) { }
        protected InsufficientBalanceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
