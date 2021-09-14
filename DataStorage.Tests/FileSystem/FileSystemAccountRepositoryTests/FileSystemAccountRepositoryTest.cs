using DataStorage.FileSystem;
using NUnit.Framework;

namespace DataStorage.Tests.FileSystem.FileSystemAccountRepositoryTests
{
    [TestFixture]
    public class FileSystemAccountRepositoryTest
    {
        protected FileSystemAccountRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new FileSystemAccountRepository();
        }
    }

    public class Create : FileSystemAccountRepositoryTest
    {
        [Test]
        public void NewAccountMustBeCreatedForUser()
        {
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

    public class GetAccountById : FileSystemAccountRepositoryTest
    {
        [Test] public void NotExistingAccountMustThrowException() { }
        [Test] public void ExistingAccountMustBeReturned() { }
    }

    public class GetAccountsByUserId : FileSystemAccountRepositoryTest
    {
        [Test] public void UserWithoutAccountsMustReturnEmptyList() { }
        [Test] public void UserWithOneAccountMustReturnTheAccount() { }
        [Test] public void UserWithMultipleAccountsMustReturnAllAccounts() { }
        [Test] public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser() { }
    }

    public class GetAccountsByUserName : FileSystemAccountRepositoryTest
    {
        [Test] public void UserWithoutAccountsMustReturnEmptyList() { }
        [Test] public void UserWithOneAccountMustReturnTheAccount() { }
        [Test] public void UserWithMultipleAccountsMustReturnAllAccounts() { }
        [Test] public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser() { }
        [Test] public void UserNameWithDifferentSpellingMustReturnAllAcounts() { }
    }
}
