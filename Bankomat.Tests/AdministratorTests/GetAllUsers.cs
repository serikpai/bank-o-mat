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
            var sut = MockAggregator.NewAdministration();
            var hash = MockAggregator.NewHashComputer();

            sut.CreateUser("susan", "12345");
            sut.CreateUser("peter", "23456");
            sut.CreateUser("john", "34567");

            var users = sut.GetAllUsers();

            users.Should().SatisfyRespectively(
                susan =>
                {
                    var pinHash = hash.Hashify("12345");
                    susan.Username.Should().Be("susan");
                    susan.Pin.Should().Be(pinHash);
                },
                peter =>
                {
                    var pinHash = hash.Hashify("23456");
                    peter.Username.Should().Be("peter");
                    peter.Pin.Should().Be(pinHash);
                },
                john =>
                {
                    var pinHash = hash.Hashify("34567");
                    john.Username.Should().Be("john");
                    john.Pin.Should().Be(pinHash);
                });

        }
    }
}
