using DataStorage.Abstractions.Exceptions;
using DataStorage.Sqlite;
using Domain;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace DataStorage.Tests.Sqlite.SqliteAccountRepositoryTests
{
    [TestFixture]
    public class SqliteAccountRepositoryTest
    {
        protected SqliteAccountRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new SqliteAccountRepository();
        }
    }

    public class Create : SqliteAccountRepositoryTest
    {
        [Test]
        public void NewAccountMustBeCreatedForUser()
        {
            var newAccount = new Account(1, "john", "debit card");

            _repository.Create(newAccount);

            newAccount.Id.Should().Be(1);
            _repository.AccountCount.Should().Be(1);
        }

        [Test]
        public void AccountWithoutUserIdMustThrowException()
        {
            var newAccount = new Account(0, "john", "debit card");

            Action act = () => _repository.Create(newAccount);

            act.Should().ThrowExactly<AccountIdMustNotBeZeroException>();
        }

        [Test]
        public void AccountWithBalanceMustThrowException()
        {
            var newAccount = new Account(1, "john", "debit card");
            newAccount.Deposit(42);

            Action act = () => _repository.Create(newAccount);

            act.Should().ThrowExactly<BalanceMustBeZeroDuringCreationException>();
        }
    }

    public class GetAccountById : SqliteAccountRepositoryTest
    {
        [Test]
        public void NotExistingAccountMustThrowException()
        {
            Action act = () => _repository.GetAccountById(42);

            act.Should().ThrowExactly<AccountIdNotFoundException>();
        }

        [Test]
        public void ExistingAccountMustBeReturned()
        {
            var account = new Account(2, "john", "debit card");
            _repository.Create(account);

            var output = _repository.GetAccountById(1);

            output.UserId.Should().Be(2);
            output.Id.Should().Be(1);
            output.GetBalance().Should().Be(0);
            output.Description.Should().Be("debit card");
        }
    }

    public class GetAccountsByUserId : SqliteAccountRepositoryTest
    {
        [Test]
        public void UserWithoutAccountsMustReturnEmptyList()
        {
            var accounts = _repository.GetAccountsByUserId(42);
            accounts.Should().BeEmpty();
        }

        [Test]
        public void UserWithOneAccountMustReturnTheAccount()
        {
            _repository.Create(new Account(11, "john", "credit card"));

            var accounts = _repository.GetAccountsByUserId(11);

            accounts.Should().HaveCount(1);
            accounts.Should().SatisfyRespectively(
                acc =>
                {
                    acc.UserId.Should().Be(11);
                    acc.Username.Should().Be("john");
                    acc.Description.Should().Be("credit card");
                });
        }

        [Test]
        public void UserWithMultipleAccountsMustReturnAllAccounts()
        {
            _repository.Create(new Account(11, "john", "credit card"));
            _repository.Create(new Account(11, "john", "debit card"));

            var accounts = _repository.GetAccountsByUserId(11);

            accounts.Should().HaveCount(2);
            accounts.Should().SatisfyRespectively(
                credit =>
                {
                    credit.UserId.Should().Be(11);
                    credit.Username.Should().Be("john");
                    credit.Description.Should().Be("credit card");
                },
                debit =>
                {
                    debit.UserId.Should().Be(11);
                    debit.Username.Should().Be("john");
                    debit.Description.Should().Be("debit card");
                });
        }
        [Test]
        public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser()
        {
            _repository.Create(new Account(11, "john", "credit card"));
            _repository.Create(new Account(12, "sue", "credit card"));
            _repository.Create(new Account(11, "john", "debit card"));

            var accounts = _repository.GetAccountsByUserId(11);

            accounts.Should().HaveCount(2);
            accounts.Should().SatisfyRespectively(
                credit =>
                {
                    credit.UserId.Should().Be(11);
                    credit.Username.Should().Be("john");
                    credit.Description.Should().Be("credit card");
                },
                debit =>
                {
                    debit.UserId.Should().Be(11);
                    debit.Username.Should().Be("john");
                    debit.Description.Should().Be("debit card");
                });
        }
    }

    public class GetAccountsByUserName : SqliteAccountRepositoryTest
    {
        [Test]
        public void UserWithoutAccountsMustReturnEmptyList()
        {
            var accounts = _repository.GetAccountsByUserName("john");
            accounts.Should().BeEmpty();
        }

        [Test]
        public void UserWithOneAccountMustReturnTheAccount()
        {
            _repository.Create(new Account(11, "john", "credit card"));

            var accounts = _repository.GetAccountsByUserName("john");

            accounts.Should().HaveCount(1);
            accounts.Should().SatisfyRespectively(
                acc =>
                {
                    acc.UserId.Should().Be(11);
                    acc.Username.Should().Be("john");
                    acc.Description.Should().Be("credit card");
                });
        }

        [Test]
        public void UserWithMultipleAccountsMustReturnAllAccounts()
        {
            _repository.Create(new Account(11, "john", "credit card"));
            _repository.Create(new Account(11, "john", "debit card"));

            var accounts = _repository.GetAccountsByUserName("john");

            accounts.Should().HaveCount(2);
            accounts.Should().SatisfyRespectively(
                credit =>
                {
                    credit.UserId.Should().Be(11);
                    credit.Username.Should().Be("john");
                    credit.Description.Should().Be("credit card");
                },
                debit =>
                {
                    debit.UserId.Should().Be(11);
                    debit.Username.Should().Be("john");
                    debit.Description.Should().Be("debit card");
                });
        }

        [Test]
        public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser()
        {
            _repository.Create(new Account(11, "john", "credit card"));
            _repository.Create(new Account(12, "sue", "credit card"));
            _repository.Create(new Account(11, "john", "debit card"));

            var accounts = _repository.GetAccountsByUserName("john");

            accounts.Should().HaveCount(2);
            accounts.Should().SatisfyRespectively(
                credit =>
                {
                    credit.UserId.Should().Be(11);
                    credit.Username.Should().Be("john");
                    credit.Description.Should().Be("credit card");
                },
                debit =>
                {
                    debit.UserId.Should().Be(11);
                    debit.Username.Should().Be("john");
                    debit.Description.Should().Be("debit card");
                });
        }

        [Test]
        public void UserNameWithDifferentSpellingMustReturnAllAcounts()
        {
            _repository.Create(new Account(11, "john", "credit card"));
            _repository.Create(new Account(12, "sue", "credit card"));
            _repository.Create(new Account(11, "john", "debit card"));

            var accounts = _repository.GetAccountsByUserName("John");

            accounts.Should().HaveCount(2);
            accounts.Should().SatisfyRespectively(
                credit =>
                {
                    credit.UserId.Should().Be(11);
                    credit.Username.Should().Be("john");
                    credit.Description.Should().Be("credit card");
                },
                debit =>
                {
                    debit.UserId.Should().Be(11);
                    debit.Username.Should().Be("john");
                    debit.Description.Should().Be("debit card");
                });
        }
    }
}
