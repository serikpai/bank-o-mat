namespace StaticProxy.Abstractions
{
    public interface IFileSystem
    {
        bool Exists(string path);
        void WriteAllText(string path, string content);
        string ReadAllText(string path);
        void WriteAllLines(string path, string[] content);
        string[] ReadAllLines(string path);
        string[] GetFiles(string path);
    }
}
