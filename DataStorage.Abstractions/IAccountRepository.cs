using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStorage.Abstractions
{
    public interface IAccountRepository
    {
        void Create(Account account);
        IEnumerable<Account> GetAccountsForUser(string username);
    }
}
