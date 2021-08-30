using Bankomat.Abstractions.Exceptions;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.UserManagerTests
{
    [TestFixture]
    public class DepositMoney : UserManagerTest
    {
        [Test]
        public void OnNotExistingAccountMustThrowAnException()
        {
            Action act = () => _underTest.DepositMoney(42, 10);

            act.Should().Throw<AccountNotExistsException>();
        }

        [Test]
        public void OnExistingAccountMustIncreaseTheBalance()
        {
            Action act = () => _underTest.DepositMoney(1, 13.37m);

            act.Should().NotThrow();

            var account = _accounts.GetAccountById(1);
            account.GetBalance().Should().Be(13.37m);
        }


        [Test]
        public void NegativeAmountMustThrowException()
        {
            Action act = () => _underTest.DepositMoney(1, -10);

            act.Should().Throw<CannotDepositNegativeAmountException>()
                .WithMessage("You are trying to deposit -10€ on account 'id:1'. This ain't working!");
        }
    }
}
