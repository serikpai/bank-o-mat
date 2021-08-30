using Cryptography;
using DataStorage.InMemory;
using NUnit.Framework;

namespace Bankomat.Tests.UserManagerTests
{
    [TestFixture]
    public abstract class UserManagerTest
    {
        protected UserManager _underTest;

        [SetUp]
        public void Setup()
        {
            var accounts = new InMemoryAccountRepository();
            var users = new InMemoryUserRepository();
            var hash = new Md5HashComputer();

            var admin = new Administration(users, accounts, hash);
            admin.CreateUser("john", "12345");


            _underTest = new UserManager(users, accounts, hash);
        }
    }
}
