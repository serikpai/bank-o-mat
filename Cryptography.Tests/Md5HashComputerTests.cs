using FluentAssertions;
using NUnit.Framework;

namespace Cryptography.Tests
{
    public class Md5HashComputerTests
    {
        [Test]
        public void ComputingHashShouldAlwaysReturnValidHash()
        {
            var sut = new Md5HashComputer();
            var expected = "bbb01f9b9074eda8df1ad19508be5438";

            var actually = sut.Hashify("helloWorld");

            actually.Should().Be(expected);
        }
    }
}