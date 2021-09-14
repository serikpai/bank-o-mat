using DataStorage.Abstractions.Exceptions;
using DataStorage.FileSystem;
using Domain;
using FluentAssertions;
using NUnit.Framework;
using StaticProxy.InMemory;
using System;

namespace DataStorage.Tests.FileSystem.FileSystemAccountRepositoryTests
{
    [TestFixture]
    public class FileSystemAccountRepositoryTest
    {
        protected FileSystemAccountRepository _repository;
        protected InMemoryFileSystem _fileSystem;

        [SetUp]
        public void Setup()
        {
            _fileSystem = new InMemoryFileSystem();
            _repository = new FileSystemAccountRepository(_fileSystem);
        }
    }

    public class Create : FileSystemAccountRepositoryTest
    {
        [Test]
        public void NewAccountMustBeCreatedForUser()
        {
            var newAccount = new Account(1, "john", "debit card");

            _repository.Create(newAccount);

            newAccount.Id.Should().Be(1);
            _repository.AccountCount.Should().Be(1);

            var fileContent = _fileSystem.ReadAllLines("1.account");
            fileContent.Should().BeEquivalentTo(
                "[account]",
                "Id = 1",
                "UserId = 1",
                "UserName = john",
                "Description = debit card",
                "Balance = 0",
                "IsFrozen = False"
                );
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

    public class GetAccountById : FileSystemAccountRepositoryTest
    {
        [Test]
        public void NotExistingAccountMustThrowException()
        {
            Action act = () => _repository.GetAccountById(42);

            act.Should().ThrowExactly<AccountIdNotFoundException>();
        }

        [Test]
        public void ExistingAccountBalanceWithCommaMustReturnTheAccount()
        {
            _fileSystem.WriteAllLines("1.account", new string[]
            {
                $"[account]",
                $"Id = 1",
                $"UserId = 2",
                $"UserName = susan storm",
                $"Description = debit card",
                $"Balance = 3,14",
                $"IsFrozen = False"
            });

            var output = _repository.GetAccountById(1);

            output.UserId.Should().Be(2);
            output.Id.Should().Be(1);
            output.GetBalance().Should().Be(3.14m);
            output.Username.Should().Be("susan storm");
            output.Description.Should().Be("debit card");
            output.IsFrozen().Should().BeFalse();
        }

        [Test]
        public void ExistingAccountBalanceWithDotMustReturnTheAccount()
        {
            _fileSystem.WriteAllLines("1.account", new string[]
            {
                $"[account]",
                $"Id = 1",
                $"UserId = 2",
                $"UserName = john doe",
                $"Description = debit card",
                $"Balance = 3.14",
                $"IsFrozen = True"
            });

            var output = _repository.GetAccountById(1);

            output.UserId.Should().Be(2);
            output.Id.Should().Be(1);
            output.GetBalance().Should().Be(3.14m);
            output.Username.Should().Be("john doe");
            output.Description.Should().Be("debit card");
            output.IsFrozen().Should().BeTrue();
        }

        [Test]
        public void UserIdWithWrongFormatMustThrowException()
        {
            _fileSystem.WriteAllLines("1.account", new string[]
            {
                $"[account]",
                $"UserId = two",
            });

            Action act = () => _repository.GetAccountById(1);

            act.Should().ThrowExactly<InjuredAccountException>()
                .WithMessage("Could not read 'UserId' with value 'two' within file '1.account'.");
        }

        [Test]
        public void BalanceWithWrongFormatMustThrowException()
        {
            _fileSystem.WriteAllLines("1.account", new string[]
            {
                $"[account]",
                $"Balance = none",
            });

            Action act = () => _repository.GetAccountById(1);

            act.Should().ThrowExactly<InjuredAccountException>()
                .WithMessage("Could not read 'Balance' with value 'none' within file '1.account'.");
        }

        [Test]
        public void IdWithWrongFormatMustThrowException()
        {
            _fileSystem.WriteAllLines("1.account", new string[]
            {
                $"[account]",
                $"Id = one",
            });

            Action act = () => _repository.GetAccountById(1);

            act.Should().ThrowExactly<InjuredAccountException>()
                .WithMessage("Could not read 'Id' with value 'one' within file '1.account'.");
        }


        [Test]
        public void FrozenWithWrongFormatMustThrowException()
        {
            _fileSystem.WriteAllLines("1.account", new string[]
            {
                $"[account]",
                $"IsFrozen = no",
            });

            Action act = () => _repository.GetAccountById(1);

            act.Should().ThrowExactly<InjuredAccountException>()
                .WithMessage("Could not read 'IsFrozen' with value 'no' within file '1.account'.");
        }
    }

    public class GetAccountsByUserId : FileSystemAccountRepositoryTest
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

    public class GetAccountsByUserName : FileSystemAccountRepositoryTest
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
