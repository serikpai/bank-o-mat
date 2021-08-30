using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStorage.InMemory
{
    public class InMemoryUserRepository : IUserRepository
    {
        private const StringComparison ComparisonMethod = StringComparison.OrdinalIgnoreCase;
        private HashSet<User> _dataStore = new HashSet<User>();
        private int _userCount;


        public void Create(User user)
        {
            user.Id = ++_userCount;
            _dataStore.Add(user);
        }

        public bool DoesUserExist(string username)
            => _dataStore.Any(u => u.Username.Equals(username, ComparisonMethod));

        public IEnumerable<User> GetAll()
            => _dataStore;

        public User GetByUsername(string username)
            => _dataStore.First(u => u.Username.Equals(username, ComparisonMethod));
    }
}