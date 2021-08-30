using System;

namespace Cryptography.Abstractions
{
    public interface IHashComputer
    {
        string Hashify(string value);
    }
}
