using Bankomat.Abstractions.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class CreateAccount : AdministratorTest
    {
        [TestCase("")]
        [TestCase(null)]
        public void CreatingUserWithInvalidUsernameMustThrowException(string username)
        {
            Action act = () => _underTest.CreateAccount(username, "credit card");

            act.Should().Throw<ArgumentException>("null or empty is not allowed for username or description")
                .WithMessage("Username must not be empty.");
        }

        [TestCase("")]
        [TestCase(null)]
        public void CreatingUserWithInvalidDescriptionMustThrowException(string descr)
        {
            Action act = () => _underTest.CreateAccount("john", descr);

            act.Should().Throw<ArgumentException>("null or empty is not allowed for username or description")
                .WithMessage("Description must not be empty.");
        }

        [Test]
        public void CreatingAccountForNotExistingUserMustThrowException()
        {
            Action act = () => _underTest.CreateAccount("john", "credit card");

            act.Should().Throw<UserNotExistsException>("there is no user with such a username")
                .WithMessage("User 'john' does not exist.");
        }

        [Test]
        public void CreatingAccountForExistingUserMustNotThrowAnyException()
        {
            _underTest.CreateUser("john", "12345");

            Action act = () => _underTest.CreateAccount("john", "credit card");

            act.Should().NotThrow();
        }
    }
}
