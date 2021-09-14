using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;

namespace DataStorage.Sqlite
{
    public class SqliteAccountRepository : IAccountRepository
    {
        public void Create(Account account)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccountsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccountsByUserName(string username)
        {
            throw new NotImplementedException();
        }
    }
}
