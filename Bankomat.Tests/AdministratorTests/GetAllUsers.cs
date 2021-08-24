using DataStorage.InMemory;
using FluentAssertions;
using NUnit.Framework;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class GetAllUsers
    {

        [Test]
        public void GettingAllExistingUsersShouldReturnAllUsers()
        {
            var userRepository = new InMemoryUserRepository();
            var sut = new Administration(userRepository);

            sut.CreateUser("susan", "12345");
            sut.CreateUser("peter", "23456");
            sut.CreateUser("john", "34567");

            var users = sut.GetAllUsers();

            users.Should().SatisfyRespectively(
                susan =>
                {
                    susan.Username.Should().Be("susan");
                    susan.Pin.Should().Be("12345");
                },
                peter =>
                {
                    peter.Username.Should().Be("peter");
                    peter.Pin.Should().Be("23456");
                },
                john =>
                {
                    john.Username.Should().Be("john");
                    john.Pin.Should().Be("34567");
                });

        }
    }
}
