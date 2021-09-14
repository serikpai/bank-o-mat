using DataStorage.Abstractions;
using DataStorage.Abstractions.Exceptions;
using Domain;
using System;
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
            if (account.UserId <= 0)
                throw new AccountIdMustNotBeZeroException();

            var currentBalance = account.GetBalance();
            if (currentBalance != 0)
                throw new BalanceMustBeZeroDuringCreationException();

            account.Id = ++AccountCount;
            _dataStore.Add(account);
        }

        public Account GetAccountById(int id)
        {
            try
            {
                return _dataStore.First(acc => acc.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new AccountIdNotFoundException($"Could not find account with ID: {id}", ex);
            }
        }

        public IEnumerable<Account> GetAccountsByUserId(int userId) 
            => _dataStore.Where(acc => acc.UserId == userId);

        public IEnumerable<Account> GetAccountsByUserName(string username) 
            => _dataStore.Where(acc => acc.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
}