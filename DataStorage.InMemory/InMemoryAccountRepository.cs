using DataStorage.Abstractions;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace DataStorage.InMemory
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private HashSet<Account> _dataStore = new HashSet<Account>();
        private int _accountCount;

        public void Create(Account account)
        {
            account.Id = ++_accountCount;
            _dataStore.Add(account);
        }

        public IEnumerable<Account> GetAccountsForUser(string username)
        {
            return _dataStore.Where(acc => acc.Username.Equals(username));
        }
    }
}