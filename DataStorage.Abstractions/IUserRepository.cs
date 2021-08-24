using Domain;
using System.Collections.Generic;

namespace DataStorage.Abstractions
{
    public interface IUserRepository
    {
        HashSet<User> GetAll();
        void Create(User user);
    }
}
