using System;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; private set; }
        public string Pin { get; private set; }

        public User(string username, string pin)
        {
            Username = username;
            Pin = pin;
        }
    }
}
