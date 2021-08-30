using Cryptography;
using DataStorage.InMemory;

namespace Bankomat.Aggregating
{
    public class Aggregator
    {
        public static IAdministration NewAdministration()
        {
            var users = new InMemoryUserRepository();
            var accounts = new InMemoryAccountRepository();
            var hashAlgo = new Md5HashComputer();

            return new Administration(users, accounts, hashAlgo);
        }
    }
}
