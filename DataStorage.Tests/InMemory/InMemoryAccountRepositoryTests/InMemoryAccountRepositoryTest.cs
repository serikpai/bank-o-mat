using DataStorage.InMemory;
using Domain;
using FluentAssertions;
using NUnit.Framework;

namespace DataStorage.Tests.InMemory.InMemoryAccountRepositoryTests
{
    [TestFixture]
    public class InMemoryAccountRepositoryTest
    {
        protected InMemoryAccountRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new InMemoryAccountRepository();
        }
    }

    public class Create : InMemoryAccountRepositoryTest
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
        }

        [Test]
        public void AccountWithBalanceMustThrowException()
        {
        }
    }

    public class GetAccountById : InMemoryAccountRepositoryTest
    {
        [Test] public void NotExistingAccountMustThrowException() { }
        [Test] public void ExistingAccountMustBeReturned() { }
    }

    public class GetAccountsByUserId : InMemoryAccountRepositoryTest
    {
        [Test] public void UserWithoutAccountsMustReturnEmptyList() { }
        [Test] public void UserWithOneAccountMustReturnTheAccount() { }
        [Test] public void UserWithMultipleAccountsMustReturnAllAccounts() { }
        [Test] public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser() { }
    }

    public class GetAccountsByUserName : InMemoryAccountRepositoryTest
    {
        [Test] public void UserWithoutAccountsMustReturnEmptyList() { }
        [Test] public void UserWithOneAccountMustReturnTheAccount() { }
        [Test] public void UserWithMultipleAccountsMustReturnAllAccounts() { }
        [Test] public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser() { }
        [Test] public void UserNameWithDifferentSpellingMustReturnAllAcounts() { }
    }
}
