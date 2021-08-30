using Bankomat.Abstractions.Exceptions;
using Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomat.Tests.UserManagerTests
{
    public class TransferMoney : UserManagerTest
    {
        [Test]
        public void FromUnknownAccountMustThrowException()
        {
            Action act = () => _underTest.TransferMoney(42, 2, 10);

            act.Should().Throw<AccountNotExistsException>()
                .WithMessage("account with id '42' could not be found");
        }

        [Test]
        public void ToUnknownAccountMustThrowException()
        {
            Action act = () => _underTest.TransferMoney(1, 42, 10);

            act.Should().Throw<AccountNotExistsException>()
                .WithMessage("account with id '42' could not be found");
        }

        [Test]
        public void FromExistingAccountWithoutMoneyMustThrowAnException()
        {
            Action act = () => _underTest.TransferMoney(1, 2, 10);

            act.Should().Throw<InsufficientBalanceException>()
                .WithMessage("Not enough money on your account 'id:1'. You are trying to transfer 10€ but you have only 0€");
        }

        [Test]
        public void FromExistingAccountWitNotEnoughMoneyMustThrowAnException()
        {
            _underTest.DepositMoney(1, 3.14m);

            Action act = () => _underTest.TransferMoney(1, 2, 10);

            act.Should().Throw<InsufficientBalanceException>()
                .WithMessage("Not enough money on your account 'id:1'. You are trying to transfer 10€ but you have only 3,14€");
        }

        [Test]
        public void FromExistingAccountMustDecreaseTheBalanceAndIncreaseTheOther()
        {
            _underTest.DepositMoney(1, 13.37m);

            _underTest.TransferMoney(1, 2, 1.44m);

            _accounts.GetAccountById(1).GetBalance().Should().Be(11.93m);
            _accounts.GetAccountById(2).GetBalance().Should().Be(1.44m);
        }

        [Test]
        public void NegativeAmountMustThrowException()
        {
            Action act = () => _underTest.TransferMoney(1, 2, -10);

            act.Should().Throw<CannotTransferNegativeAmountException>()
                .WithMessage("You are trying to transfer -10€ from account 'id:1'. This ain't working!");
        }
    }
}
