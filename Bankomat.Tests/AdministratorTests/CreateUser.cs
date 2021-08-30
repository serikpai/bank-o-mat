using Bankomat.Abstractions.Exceptions;
using DataStorage.InMemory;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class CreateUser : AdministratorTest
    {
        [TestCase("")]
        [TestCase(null)]
        public void CreatingUserWithInvalidUsernameMustThrowException(string username)
        {
            Action act = () => _underTest.CreateUser(username, "12345");

            act.Should().Throw<ArgumentException>("null or empty is not allowed for username or pin")
                .WithMessage("Username must not be empty.");
        }

        [TestCase("")]
        [TestCase(null)]
        public void CreatingUserWithInvalidPinMustThrowException(string pin)
        {
            Action act = () => _underTest.CreateUser("john", pin);

            act.Should().Throw<ArgumentException>("null or empty is not allowed for username or pin")
                .WithMessage("PIN must not be empty.");
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

            act.Should().Throw<UserAlreadyExistsException>("there is already an user with the same name")
                .WithMessage("User 'john' already exists.");
        }

        [Test]
        public void CreatingExistingUserAmongOtherUsersMustNotThrowAnyException()
        {
            _underTest.CreateUser("susan", "12345");
            _underTest.CreateUser("peter", "12345");
            _underTest.CreateUser("john", "12345");

            Action act = () => _underTest.CreateUser("peter", "22222");

            act.Should().Throw<UserAlreadyExistsException>("there is already an user with the same name")
                .WithMessage("User 'peter' already exists.");
        }

    }

}
