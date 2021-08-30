using Bankomat.Abstractions.Exceptions;
using Cryptography.Abstractions;
using DataStorage.Abstractions;
using Domain;
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

        public bool IsPinCorrect(string username, string pin)
        {
            if (!_users.DoesUserExist(username)) return false;

            var encryptedPin = _hash.Hashify(pin);
            var user = _users.GetByUsername(username);

            var isPinCorrect = user.Pin.Equals(encryptedPin);

            return isPinCorrect;
        }

        public void DepositMoney(int accountId, decimal amount)
        {
            var account = GetAccountOrThrow(accountId);
            account.Deposit(amount);
        }

        public void WithdrawMoney(int accountId, decimal amount)
        {
            var account = GetAccountOrThrow(accountId);
            account.Withdraw(amount);
        }

        public void TransferMoney(int senderAccountId, int receiverAccountId, decimal amount)
        {
            var sender = GetAccountOrThrow(senderAccountId);
            var receiver = GetAccountOrThrow(receiverAccountId);

            sender.Transfer(amount, receiver);
        }

        private Account GetAccountOrThrow(int accountId)
        {
            try
            {
                return _accounts.GetAccountById(accountId);
            }
            catch (InvalidOperationException ex)
            {
                throw new AccountNotExistsException($"account with id '{accountId}' could not be found", ex);
            }
        }
    }
}