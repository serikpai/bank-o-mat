using DataStorage.Sqlite;
using NUnit.Framework;

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
        [Test] public void NewAccountMustBeCreatedForUser() { }
        [Test] public void AccountWithoutUserIdMustThrowException() { }
        [Test] public void AccountWithBalanceMustThrowException() { }

    }

    public class GetAccountById : SqliteAccountRepositoryTest
    {
        [Test] public void NotExistingAccountMustThrowException() { }
        [Test] public void ExistingAccountMustBeReturned() { }
    }

    public class GetAccountsByUserId : SqliteAccountRepositoryTest
    {
        [Test] public void UserWithoutAccountsMustReturnEmptyList() { }
        [Test] public void UserWithOneAccountMustReturnTheAccount() { }
        [Test] public void UserWithMultipleAccountsMustReturnAllAccounts() { }
        [Test] public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser() { }
    }

    public class GetAccountsByUserName : SqliteAccountRepositoryTest
    {
        [Test] public void UserWithoutAccountsMustReturnEmptyList() { }
        [Test] public void UserWithOneAccountMustReturnTheAccount() { }
        [Test] public void UserWithMultipleAccountsMustReturnAllAccounts() { }
        [Test] public void MultipleAccountsForSomeUsersMustOnlyReturnTheAccountOfOneUser() { }
        [Test] public void UserNameWithDifferentSpellingMustReturnAllAcounts() { }
    }
}
