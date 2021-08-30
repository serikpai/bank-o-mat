using Cryptography.Abstractions;
using DataStorage.Abstractions;
using System;

namespace Bankomat
{
    public class UserManager
    {
        private readonly IUserRepository _users;
        private readonly IAccountRepository _accountRepository;
        private readonly IHashComputer _hash;

        public UserManager(IUserRepository userRepository, IAccountRepository accountRepository, IHashComputer hash)
        {
            _users = userRepository;
            _accountRepository = accountRepository;
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
    }
}