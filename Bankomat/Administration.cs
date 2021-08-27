using Bankomat.Abstractions.Exceptions;
using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankomat
{
    public class Administration
    {
        private const string UserNameIsEmptyError = "Username must not be empty.";
        private const string PinIsEmptyError = "PIN must not be empty.";
        private const string DescriptionIsEmptyError = "Description must not be empty.";

        private readonly IUserRepository _users;
        private readonly IAccountRepository _accounts;

        public Administration(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _users = userRepository;
            _accounts = accountRepository;
        }

        public IEnumerable<User> GetAllUsers()
            => _users.GetAll();

        public void CreateUser(string username, string pin)
        {
            ThrowIfNullOrEmpty(username, UserNameIsEmptyError);
            ThrowIfNullOrEmpty(pin, PinIsEmptyError);
            ThrowIfUserAlreadyExist(username);

            var newUser = new User(username, pin);
            _users.Create(newUser);
        }

        public void CreateAccount(string username, string description)
        {
            ThrowIfNullOrEmpty(username, UserNameIsEmptyError);
            ThrowIfNullOrEmpty(description, DescriptionIsEmptyError);
            ThrowIfUserDoesNotExist(username);

            _accounts.Create(new Account(username, description));
        }

        private bool DoesUserExist(string username)
            => GetAllUsers()
                .Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Account> GetAccountsForUser(string username)
        {
            ThrowIfNullOrEmpty(username, UserNameIsEmptyError);
            ThrowIfUserDoesNotExist(username);

            return _accounts.GetAccountsForUser(username);
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
            if (!DoesUserExist(username))
            {
                throw new UserNotExistsException($"User '{username}' does not exist.");
            }
        }

        private void ThrowIfUserAlreadyExist(string username)
        {
            if (DoesUserExist(username))
            {
                throw new UserAlreadyExistsException($"User '{username}' already exists.");
            }
        }
    }
}