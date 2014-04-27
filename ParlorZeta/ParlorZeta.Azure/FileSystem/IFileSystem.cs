using System.IO;

namespace ParlorZeta.Azure.FileSystem
{
    public interface IFileSystem
    {
        void SaveFile(Stream fileStream, string appdataPublishsettings, string fileName);
    }
}