using StaticProxy.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaticProxy.InMemory
{
    public class InMemoryFileSystem : IFileSystem
    {
        private Dictionary<string, string> _fileStore = new Dictionary<string, string>();

        public bool Exists(string path)
        {
            return _fileStore.ContainsKey(path);
        }

        public string[] GetFiles(string path)
        {
            return _fileStore.Keys.ToArray();
        }

        public string[] ReadAllLines(string path)
        {
            return ReadAllText(path).Split('\n');
        }

        public string ReadAllText(string path)
        {
            return _fileStore[path];
        }

        public void WriteAllLines(string path, string[] content)
        {
            WriteAllText(path, string.Join("\n", content));
        }

        public void WriteAllText(string path, string content)
        {
            _fileStore.Add(path, content);
        }
    }
}