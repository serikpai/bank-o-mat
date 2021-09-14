﻿using DataStorage.Abstractions;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace DataStorage.InMemory
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly HashSet<Account> _dataStore = new HashSet<Account>();
        public int AccountCount { get; private set; }

        public void Create(Account account)
        {
            account.Id = ++AccountCount;
            _dataStore.Add(account);
        }

        public Account GetAccountById(int id) 
            => _dataStore.First(acc => acc.Id == id);

        public IEnumerable<Account> GetAccountsByUserId(int userId) 
            => _dataStore.Where(acc => acc.UserId == userId);

        public IEnumerable<Account> GetAccountsByUserName(string username) 
            => _dataStore.Where(acc => acc.Username.Equals(username));
    }
}