using Cryptography;
using Cryptography.Abstractions;
using DataStorage.InMemory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public abstract class AdministratorTest
    {
        protected Administration _underTest;
        protected IHashComputer _hash;

        [SetUp]
        public void Setup()
        {
            var accounts = new InMemoryAccountRepository();
            var users = new InMemoryUserRepository();
            _hash = new Md5HashComputer();

            _underTest = new Administration(users, accounts, _hash);
        }
    }
}
