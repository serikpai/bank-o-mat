using DataStorage.InMemory;

namespace Bankomat.Tests
{
    class MockAggregator
    {
        public static Administration NewAdministration()
        {
            var users = new InMemoryUserRepository();
            var accounts = new InMemoryAccountRepository();

            return new Administration(users, accounts);
        }
    }
}
