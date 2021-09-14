using System;

namespace DataStorage.Abstractions.Exceptions
{
    [Serializable]
    public class BalanceMustBeZeroDuringCreationException : Exception
    {
        public BalanceMustBeZeroDuringCreationException() { }
        public BalanceMustBeZeroDuringCreationException(string message) : base(message) { }
        public BalanceMustBeZeroDuringCreationException(string message, Exception inner) : base(message, inner) { }
        protected BalanceMustBeZeroDuringCreationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
