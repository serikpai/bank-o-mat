using Bankomat.Abstractions.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class GetAccountsForUser : AdministratorTest
    {
        [TestCase("")]
        [TestCase(null)]
        public void GetAccountForEmptyOrNullUserMustThrowException(string username)
        {
            Action act = () => _underTest.GetAccountsForUser(username);

            act.Should().Throw<ArgumentException>("username shouldn't be null or empty!")
                .WithMessage("Username must not be empty.");
        }

        [Test]
        public void GetAcccountsForNotExistingUserMustThrowException()
        {
            Action act = () => _underTest.GetAccountsForUser("john");

            act.Should().Throw<UserNotExistsException>("there is no user with such a username")
                .WithMessage("User 'john' does not exist.");
        }

        [Test]
        public void GetAccountForExistingUserButWithoutAccountMustReturnEmptyAccountList()
        {
            _underTest.CreateUser("john", "12345");

            var accounts = _underTest.GetAccountsForUser("john");

            accounts.Should().BeEmpty("there is currently no accounts for john available");
        }

        [Test]
        public void GetAcccountForExistingUserWithAccountsMustReturnEveryAccount()
        {
            _underTest.CreateUser("john", "12345");
            _underTest.CreateAccount("john", "credit card");
            _underTest.CreateAccount("john", "debit card");

            var accounts = _underTest.GetAccountsForUser("john");

            accounts.Should().SatisfyRespectively(
                cc =>
                {
                    cc.Username.Should().Be("john");
                    cc.Description.Should().Be("credit card");
                    cc.UserId.Should().Be(1);
                    cc.Id.Should().Be(1);
                },
                dc =>
                {
                    dc.Username.Should().Be("john");
                    dc.Description.Should().Be("debit card");
                    dc.UserId.Should().Be(1);
                    dc.Id.Should().Be(2);
                });
        }

        [Test]
        public void GetAccountForOneUserIfMultipleUsersAreWithinTheSystem()
        {
            _underTest.CreateUser("john", "12345");
            _underTest.CreateUser("susan", "12345");
            _underTest.CreateAccount("john", "credit card");
            _underTest.CreateAccount("john", "debit card");
            _underTest.CreateAccount("susan", "credit card");

            var accounts = _underTest.GetAccountsForUser("john");

            accounts.Should().SatisfyRespectively(
                cc =>
                {
                    cc.Username.Should().Be("john");
                    cc.Description.Should().Be("credit card");
                    cc.UserId.Should().Be(1);
                    cc.Id.Should().Be(1);
                },
                dc =>
                {
                    dc.Username.Should().Be("john");
                    dc.Description.Should().Be("debit card");
                    dc.UserId.Should().Be(1);
                    dc.Id.Should().Be(2);
                });
        }
    }
}
