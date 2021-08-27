using Bankomat.Abstractions.Exceptions;
using DataStorage.InMemory;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class CreateUser
    {
        [Test]
        public void CreatingTheFirstUserMustNotThrowAnyException()
        {
            var sut = MockAggregator.NewAdministration();

            Action act = () => sut.CreateUser("john", "12345");

            act.Should().NotThrow("there are no users there and we are not expecting any issues.");
        }

        [Test]
        public void CreatingAnotherUserMustNotThrowAnyException()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("john", "12345");

            Action act = () => sut.CreateUser("peter", "22222");

            act.Should().NotThrow("there is no user with the same name.");
        }

        [Test]
        public void CreatingUserWithTheSameUsernameMustThrowAnException()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("john", "12345");

            Action act = () => sut.CreateUser("john", "11111");

            act.Should().Throw<UserAlreadyExistsException>("there is already an user with the same name");
        }

        [Test]
        public void CreatingExistingUserAmongOtherUsersMustNotThrowAnyException()
        {
            var sut = MockAggregator.NewAdministration();
            sut.CreateUser("susan", "12345");
            sut.CreateUser("peter", "12345");
            sut.CreateUser("john", "12345");

            Action act = () => sut.CreateUser("peter", "22222");

            act.Should().Throw<UserAlreadyExistsException>("there is already an user with the same name");
        }

    }

}
