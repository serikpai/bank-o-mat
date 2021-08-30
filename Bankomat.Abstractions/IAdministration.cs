using Domain;
using System.Collections.Generic;

namespace Bankomat
{
    public interface IAdministration
    {
        void CreateAccount(string username, string description);
        void CreateUser(string username, string pin);
        IEnumerable<Account> GetAccountsForUser(string username);
        IEnumerable<User> GetAllUsers();
    }
}