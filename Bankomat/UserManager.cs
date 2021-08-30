using Bankomat.Abstractions.Exceptions;
using Cryptography.Abstractions;
using DataStorage.Abstractions;
using System;

namespace Bankomat
{
    public class UserManager
    {
        private readonly IUserRepository _users;
        private readonly IAccountRepository _accounts;
        private readonly IHashComputer _hash;

        public UserManager(IUserRepository userRepository, IAccountRepository accountRepository, IHashComputer hash)
        {
            _users = userRepository;
            _accounts = accountRepository;
            _hash = hash;
        }

        public void DepositMoney(int accountId, decimal value)
        {
            try
            {
                var account = _accounts.GetAccountById(accountId);
                account.Deposit(value);
            }
            catch (InvalidOperationException ex)
            {
                throw new AccountNotExistsException($"account with id '{accountId}' could not be found", ex);
            }
        }

        public bool IsPinCorrect(string username, string pin)
        {
            if (!_users.DoesUserExist(username)) return false;

            var encryptedPin = _hash.Hashify(pin);
            var user = _users.GetByUsername(username);

            var isPinCorrect = user.Pin.Equals(encryptedPin);

            return isPinCorrect;
        }

        public void WithdrawMoney(int accountId, decimal value)
        {
            try
            {
                var account = _accounts.GetAccountById(accountId);
                account.Withdraw(value);
            }
            catch (InvalidOperationException ex)
            {
                throw new AccountNotExistsException($"account with id '{accountId}' could not be found", ex);
            }
        }
    }
}