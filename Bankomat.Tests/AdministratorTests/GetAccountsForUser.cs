using Bankomat.Abstractions.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class GetAccountsForUser
    {
        [TestCase("")]
        [TestCase(null)]
        public void GetAccountForEmptyOrNullUserMustThrowException(string username)
        {
            var sut = MockAggregator.NewAdministration();

            Action act = () => sut.GetAccountsForUser(username);

            act.Should().Throw<ArgumentException>("username shouldn't be null or empty!")
                .WithMessage("Username must not be empty.");
        }

        [Test]
        public void GetAcccountsForNotExistingUserMustThrowException()
        {
            var sut = MockAggregator.NewAdministration();

            Action act = () => sut.GetAccountsForUser("john");

            act.Should().Throw<UserNotExistsException>("there is no user with such a username")
                .WithMessage("User 'john' does not exist.");
        }

        [Test]
        public void GetAccountForExistingUserButWithoutAccountMustReturnEmptyAccountList()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("john", "12345");

            var accounts = sut.GetAccountsForUser("john");

            accounts.Should().BeEmpty("there is currently no accounts for john available");
        }

        [Test]
        public void GetAcccountForExistingUserWithAccountsMustReturnEveryAccount()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("john", "12345");
            sut.CreateAccount("john", "credit card");
            sut.CreateAccount("john", "debit card");

            var accounts = sut.GetAccountsForUser("john");

            accounts.Should().SatisfyRespectively(
                cc =>
                {
                    cc.Username.Should().Be("john");
                    cc.Description.Should().Be("credit card");
                },
                dc =>
                {
                    dc.Username.Should().Be("john");
                    dc.Description.Should().Be("debit card");
                });
        }

        [Test]
        public void GetAccountForOneUserIfMultipleUsersAreWithinTheSystem()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("john", "12345");
            sut.CreateUser("susan", "12345");
            sut.CreateAccount("john", "credit card");
            sut.CreateAccount("john", "debit card");
            sut.CreateAccount("susan", "credit card");

            var accounts = sut.GetAccountsForUser("john");

            accounts.Should().SatisfyRespectively(
                cc =>
                {
                    cc.Username.Should().Be("john");
                    cc.Description.Should().Be("credit card");
                },
                dc =>
                {
                    dc.Username.Should().Be("john");
                    dc.Description.Should().Be("debit card");
                });
        }
    }
}
