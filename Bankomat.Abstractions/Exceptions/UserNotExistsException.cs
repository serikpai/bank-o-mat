using System;

namespace Bankomat.Abstractions.Exceptions
{
    [Serializable]
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException()
        {
        }
        
        public UserNotExistsException(string message) : base(message) 
        {
        }
        
        public UserNotExistsException(string message, Exception inner) : base(message, inner) 
        {
        }
    }
}