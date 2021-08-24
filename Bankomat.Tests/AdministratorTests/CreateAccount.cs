using Bankomat.Abstractions.Exceptions;
using DataStorage.InMemory;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class CreateAccount
    {
        Administration _underTest;

        [SetUp]
        public void Setup()
        {
            var users = new InMemoryUserRepository();

            _underTest = new Administration(users);
        }


        [Test]
        public void CreatingAccountForExistingUserMustNotThrowAnyException()
        {
            _underTest.CreateUser("peter", "12345");

            Action act = () => _underTest.CreateAccount("peter", "credit card");

            act.Should().NotThrow();
        }

        [Test]
        public void CreatingAccountForNotExistingUserMustThrowException()
        {
            Action act = () => _underTest.CreateAccount("peter", "credit card");

            act.Should().Throw<UserNotExistsException>("there is no user with such a username");
        }
    }
}
