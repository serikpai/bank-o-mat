using Cryptography;
using DataStorage.Abstractions;
using DataStorage.InMemory;
using NUnit.Framework;

namespace Bankomat.Tests.UserManagerTests
{
    [TestFixture]
    public abstract class UserManagerTest
    {
        protected UserManager _underTest;
        protected IAccountRepository _accounts;

        [SetUp]
        public void Setup()
        {
            _accounts = new InMemoryAccountRepository();
            var users = new InMemoryUserRepository();
            var hash = new Md5HashComputer();

            var admin = new Administration(users, _accounts, hash);
            admin.CreateUser("john", "12345");
            admin.CreateAccount("john", "Debit Card");

            _underTest = new UserManager(users, _accounts, hash);
        }
    }
}
