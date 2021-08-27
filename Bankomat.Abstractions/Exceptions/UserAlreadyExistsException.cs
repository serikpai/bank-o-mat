using System;

namespace Bankomat.Abstractions.Exceptions
{
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() 
        {
        }

        public UserAlreadyExistsException(string message) : base(message) 
        { 
        }
        
        public UserAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}