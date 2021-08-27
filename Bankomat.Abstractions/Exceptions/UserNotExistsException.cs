using System;

namespace Bankomat.Abstractions.Exceptions
{
    [Serializable]
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException()
        {
        }
        
        public UserNotExistsException(string username) : base(CreateMessage(username)) 
        {
        }
        
        public UserNotExistsException(string username, Exception inner) : base(CreateMessage(username), inner) 
        {
        }
        
        private static string CreateMessage(string username)
        {
            return $"User '{username}' does not exist.";
        }
    }
}