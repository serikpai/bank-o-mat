using StaticProxy.Abstractions;
using System.IO;

namespace StaticProxy.SystemIo
{
    public class SystemIoFileSystem : IFileSystem
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public string ReadAllText(string path)
        {
            
            return File.ReadAllText(path);
        }

        public void WriteAllLines(string path, string[] content)
        {
            File.WriteAllLines(path, content);
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
            
        }
    }
}
