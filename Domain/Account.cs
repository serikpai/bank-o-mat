namespace Domain
{
    public class Account
    {
        public Account(int userId, string username, string description)
        {
            UserId = userId;
            Username = username;
            Description = description;
        }

        public int Id { get; set; }
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string Description { get; private set; }
    }
}
