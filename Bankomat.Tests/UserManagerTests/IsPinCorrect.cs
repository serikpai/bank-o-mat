using FluentAssertions;
using NUnit.Framework;

namespace Bankomat.Tests.UserManagerTests
{
    public class IsPinCorrect : UserManagerTest
    {
        [Test]
        public void WithUnknownUserMustReturnFalse()
        {
            var result = _underTest.IsPinCorrect("susan", "12345");

            result.Should().BeFalse();
        }

        [Test]
        public void WithWrongPinMustReturnFalse()
        {
            var result = _underTest.IsPinCorrect("john", "147258");

            result.Should().BeFalse();
        }

        [Test]
        public void WithCorrectUserAndPinMustReturnTrue()
        {
            var result = _underTest.IsPinCorrect("john", "12345");

            result.Should().BeTrue();
        }
    }
}