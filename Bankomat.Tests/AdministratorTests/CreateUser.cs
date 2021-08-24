using Bankomat.Abstractions.Exceptions;
using DataStorage.InMemory;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class CreateUser
    {
        Administration _underTest;

        [SetUp]
        public void Setup()
        {
            var users = new InMemoryUserRepository();

            _underTest = new Administration(users);
        }

        [Test]
        public void CreatingTheFirstUserMustNotThrowAnyException()
        {
            Action act = () => _underTest.CreateUser("john", "12345");

            act.Should().NotThrow("there are no users there and we are not expecting any issues.");
        }

        [Test]
        public void CreatingAnotherUserMustNotThrowAnyException()
        {
            _underTest.CreateUser("john", "12345");

            Action act = () => _underTest.CreateUser("peter", "22222");

            act.Should().NotThrow("there is no user with the same name.");
        }

        [Test]
        public void CreatingUserWithTheSameUsernameMustThrowAnException()
        {
            _underTest.CreateUser("john", "12345");

            Action act = () => _underTest.CreateUser("john", "11111");

            act.Should().Throw<UserAlreadyExistsException>("there is already an user with the same name");
        }

        [Test]
        public void CreatingExistingUserAmongOtherUsersMustNotThrowAnyException()
        {
            _underTest.CreateUser("susan", "12345");
            _underTest.CreateUser("peter", "12345");
            _underTest.CreateUser("john", "12345");

            Action act = () => _underTest.CreateUser("peter", "22222");

            act.Should().Throw<UserAlreadyExistsException>("there is already an user with the same name");
        }

    }

}
