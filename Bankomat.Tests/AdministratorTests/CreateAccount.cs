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
        [TestCase("")]
        [TestCase(null)]
        public void CreatingUserWithInvalidUsernameMustThrowException(string username)
        {
            var sut = MockAggregator.NewAdministration();

            Action act = () => sut.CreateAccount(username, "credit card");

            act.Should().Throw<ArgumentException>("null or empty is not allowed for username or description")
                .WithMessage("Username must not be empty.");
        }

        [TestCase("")]
        [TestCase(null)]
        public void CreatingUserWithInvalidDescriptionMustThrowException(string descr)
        {
            var sut = MockAggregator.NewAdministration();

            Action act = () => sut.CreateAccount("john", descr);

            act.Should().Throw<ArgumentException>("null or empty is not allowed for username or description")
                .WithMessage("Description must not be empty.");
        }

        [Test]
        public void CreatingAccountForNotExistingUserMustThrowException()
        {
            var sut = MockAggregator.NewAdministration();
            Action act = () => sut.CreateAccount("john", "credit card");

            act.Should().Throw<UserNotExistsException>("there is no user with such a username")
                .WithMessage("User 'john' does not exist.");
        }

        [Test]
        public void CreatingAccountForExistingUserMustNotThrowAnyException()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("john", "12345");

            Action act = () => sut.CreateAccount("john", "credit card");

            act.Should().NotThrow();
        }
    }
}
