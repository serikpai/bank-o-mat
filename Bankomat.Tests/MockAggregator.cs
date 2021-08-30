using Cryptography;
using Cryptography.Abstractions;
using DataStorage.InMemory;

namespace Bankomat.Tests
{
    class MockAggregator
    {
        public static Administration NewAdministration()
        {
            var users = new InMemoryUserRepository();
            var accounts = new InMemoryAccountRepository();
            var hash = NewHashComputer();

            return new Administration(users, accounts, hash);
        }
        
        public static IHashComputer NewHashComputer()
        {
            return new Md5HashComputer();
        }
    }
}
