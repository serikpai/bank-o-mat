using Domain;
using System.Collections.Generic;

namespace DataStorage.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        void Create(User user);
        User GetByUsername(string username);
        bool DoesUserExist(string username);
    }
}
