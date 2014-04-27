using System.Collections.Generic;
using System.IO;

namespace ParlorZeta.Azure.FileSystem
{
    public interface IFileSystem
    {        
        Stream OpenWritableFileStream(string relativePath, string fileName);
        IEnumerable<string> GetFileNames(string relativePath);
        void Delete(string fileName);
    }
}