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
        [Test]
        public void CreatingAccountForExistingUserMustNotThrowAnyException()
        {
            var underTest = MockAggregator.NewAdministration();
            underTest.CreateUser("peter", "12345");

            Action act = () => underTest.CreateAccount("peter", "credit card");

            act.Should().NotThrow();
        }

        [Test]
        public void CreatingAccountForNotExistingUserMustThrowException()
        {
            var underTest = MockAggregator.NewAdministration();
            Action act = () => underTest.CreateAccount("peter", "credit card");

            act.Should().Throw<UserNotExistsException>("there is no user with such a username");
        }
    }
}
