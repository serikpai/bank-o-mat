using Cryptography.Abstractions;
using System;
using System.IO;
using System.Text;

namespace Cryptography
{
    public class Md5HashComputer : IHashComputer
    {
        private const string Salt = "Bako-O-Mat";

        public string Hashify(string value)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var saltedValue = value + Salt;
                var sourceBytes = Encoding.UTF8.GetBytes(saltedValue);
                var hashedBytes = md5.ComputeHash(sourceBytes);

                var convertedToHexValues = BitConverter.ToString(hashedBytes);
                var withoutSeparatorLowerCased = convertedToHexValues.Replace("-","").ToLower();
                return withoutSeparatorLowerCased;
            }
        }
    }
}
