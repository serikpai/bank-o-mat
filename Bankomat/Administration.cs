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
        private readonly IUserRepository _users;

        public Administration(IUserRepository userRepository)
        {
            _users = userRepository;
        }

        public HashSet<User> GetAllUsers()
            => _users.GetAll();

        public void CreateUser(string username, string pin)
        {
            if (DoesUserExist(username))
            {
                throw new UserAlreadyExistsException(username);
            }

            var newUser = new User(username, pin);
            _users.Create(newUser);
        }

        public void CreateAccount(string username, string description)
        {
            if (!DoesUserExist(username))
            {
                throw new UserNotExistsException(username);
            }
        }

        private bool DoesUserExist(string username)
            => GetAllUsers()
                .Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
}