using DataStorage.Abstractions;
using Domain;
using System;
using System.Collections.Generic;

namespace DataStorage.FileSystem
{
    public class FileSystemUserRepository : IUserRepository
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
