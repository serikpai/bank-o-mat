using Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Bankomat.Tests.DomainTests
{
    [TestFixture]
    public class UserNameEncryption
    {
        [Test]
        public void SettingUserNameMustHoldEncrypedName()
        {
            var user = new User("peter", "12345");

            user.Cipher();

            user.Username.Should().Be("kvgvi");
        }
    }
}
