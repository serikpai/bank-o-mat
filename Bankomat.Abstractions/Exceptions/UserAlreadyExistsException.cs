using System;

namespace Bankomat.Abstractions.Exceptions
{
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() 
        {
        }

        public UserAlreadyExistsException(string username) : base(CreateMessage(username)) 
        { 
        }
        
        public UserAlreadyExistsException(string username, Exception inner) : base(CreateMessage(username), inner)
        {
        }
        
        private static string CreateMessage(string username)
        {
            return $"User '{username}' already exists.";
        }
    }
}