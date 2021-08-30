using Bankomat.Abstractions.Exceptions;
using Cryptography.Abstractions;
using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankomat
{
    public class Administration : IAdministration
    {
        private const string UserNameIsEmptyError = "Username must not be empty.";
        private const string PinIsEmptyError = "PIN must not be empty.";
        private const string DescriptionIsEmptyError = "Description must not be empty.";

        private readonly IUserRepository _users;
        private readonly IAccountRepository _accounts;
        private readonly IHashComputer _hash;

        public Administration(IUserRepository userRepository, IAccountRepository accountRepository, IHashComputer hash)
        {
            _users = userRepository;
            _accounts = accountRepository;
            _hash = hash;
        }

        public IEnumerable<User> GetAllUsers()
            => _users.GetAll();

        public void CreateUser(string username, string pin)
        {
            ThrowIfNullOrEmpty(username, UserNameIsEmptyError);
            ThrowIfNullOrEmpty(pin, PinIsEmptyError);
            ThrowIfUserAlreadyExist(username);

            var encryptedPin = _hash.Hashify(pin);

            var newUser = new User(username, encryptedPin);
            _users.Create(newUser);
        }

        public void CreateAccount(string username, string description)
        {
            ThrowIfNullOrEmpty(username, UserNameIsEmptyError);
            ThrowIfNullOrEmpty(description, DescriptionIsEmptyError);
            ThrowIfUserDoesNotExist(username);

            var user = _users.GetByUsername(username);

            var newAccount = new Account(user.Id, username, description);
            _accounts.Create(newAccount);
        }



        public IEnumerable<Account> GetAccountsForUser(string username)
        {
            ThrowIfNullOrEmpty(username, UserNameIsEmptyError);
            ThrowIfUserDoesNotExist(username);

            return _accounts.GetAccountsByUserName(username);
        }

        private void ThrowIfNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message);
            }
        }

        private void ThrowIfUserDoesNotExist(string username)
        {
            if (!_users.DoesUserExist(username))
            {
                throw new UserNotExistsException($"User '{username}' does not exist.");
            }
        }

        private void ThrowIfUserAlreadyExist(string username)
        {
            if (_users.DoesUserExist(username))
            {
                throw new UserAlreadyExistsException($"User '{username}' already exists.");
            }
        }
    }
}