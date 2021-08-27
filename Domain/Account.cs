namespace Domain
{
    public class Account
    {
        public Account(string username, string description)
        {
            Username = username;
            Description = description;
        }

        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string Description { get; private set; }
    }
}
