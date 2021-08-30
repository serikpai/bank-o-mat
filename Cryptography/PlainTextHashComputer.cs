using Cryptography.Abstractions;

namespace Cryptography
{
    public class PlainTextHashComputer : IHashComputer
    {
        public string Hashify(string value)
        {
            return value;
        }
    }
}