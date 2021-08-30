using DataStorage.InMemory;
using FluentAssertions;
using NUnit.Framework;

namespace Bankomat.Tests.AdministratorTests
{
    [TestFixture]
    public class GetAllUsers : AdministratorTest
    {
        [Test]
        public void GettingAllExistingUsersShouldReturnAllUsers()
        {
            _underTest.CreateUser("susan", "12345");
            _underTest.CreateUser("peter", "23456");
            _underTest.CreateUser("john", "34567");

            var users = _underTest.GetAllUsers();

            users.Should().SatisfyRespectively(
                susan =>
                {
                    var pinHash = _hash.Hashify("12345");
                    susan.Username.Should().Be("susan");
                    susan.Pin.Should().Be(pinHash);
                    susan.Id.Should().Be(1);
                },
                peter =>
                {
                    var pinHash = _hash.Hashify("23456");
                    peter.Username.Should().Be("peter");
                    peter.Pin.Should().Be(pinHash);
                    peter.Id.Should().Be(2);
                },
                john =>
                {
                    var pinHash = _hash.Hashify("34567");
                    john.Username.Should().Be("john");
                    john.Pin.Should().Be(pinHash);
                    john.Id.Should().Be(3);
                });

        }
    }
}
