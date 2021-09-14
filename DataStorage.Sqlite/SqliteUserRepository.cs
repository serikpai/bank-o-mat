using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStorage.Sqlite
{
    public class SqliteUserRepository : IUserRepository
    {
        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public bool DoesUserExist(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
