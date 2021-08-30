using Bankomat.Abstractions.Exceptions;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.UserManagerTests
{
    [TestFixture]
    public class WithdrawMoney : UserManagerTest
    {
        [Test]
        public void OnNotExistingAccountMustThrowAnException()
        {
            Action act = () => _underTest.WithdrawMoney(42, 10);

            act.Should().Throw<AccountNotExistsException>()
                .WithMessage("account with id '42' could not be found");
        }

        [Test]
        public void OnExistingAccountWithoutMoneyMustThrowAnException()
        {
            Action act = () => _underTest.WithdrawMoney(1, 10);

            act.Should().Throw<InsufficientBalanceException>()
                .WithMessage("Not enough money on your account 'id:1'. You are trying to get 10€ but you have only 0€");
        }

        [Test]
        public void OnExistingAccountWitNotEnoughMoneyMustThrowAnException()
        {
            _underTest.DepositMoney(1, 3.14m);

            Action act = () => _underTest.WithdrawMoney(1, 10);

            act.Should().Throw<InsufficientBalanceException>()
                .WithMessage("Not enough money on your account 'id:1'. You are trying to get 10€ but you have only 3,14€");
        }

        [Test]
        public void OnExistingAccountMustDecreaseTheBalance()
        {
            _underTest.DepositMoney(1, 13.37m);

            _underTest.WithdrawMoney(1, 1.44m);

            var account = _accounts.GetAccountById(1);
            account.GetBalance().Should().Be(11.93m);
        }

        [Test]
        public void NegativeAmountMustThrowException()
        {
            Action act = () => _underTest.WithdrawMoney(1, -10);

            act.Should().Throw<CannotWithdrawNegativeAmountException>()
                .WithMessage("You are trying to withdraw -10€ on account 'id:1'. This ain't working!");
        }
    }
}
