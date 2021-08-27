using System;

namespace Domain
{
    public class User
    {
        public string Username { get; private set; }
        public string Pin { get; private set; }

        public User(string username, string pin)
        {
            Username = username;
            Pin = pin;
        }
                
        //private string ReverseCharacters(string input)
        //{
        //    string output = "";

        //    foreach (var letter in input)
        //    {
        //        output += Convert.ToChar('z' - (letter - 'a'));
        //    }

        //    return output;
        //}
    }
}
