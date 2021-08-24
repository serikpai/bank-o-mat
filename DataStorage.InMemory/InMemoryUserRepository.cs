using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;

namespace DataStorage.InMemory
{
    public class InMemoryUserRepository : IUserRepository
    {
        private HashSet<User> _dataStore = new HashSet<User>();

        public void Create(User user)
        {
            _dataStore.Add(user);
        }

        public HashSet<User> GetAll()
        {
            return _dataStore;
        }
    }
}
