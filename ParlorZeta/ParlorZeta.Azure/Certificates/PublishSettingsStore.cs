using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ParlorZeta.Azure.FileSystem;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettingsStore
    {
        public PublishSettingsStore(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void SaveSettings(string fileName, Stream inputFileStream)
        {           
            try
            {
                var document = XDocument.Load(inputFileStream);
                if (document.Descendants("Subscription").Any())
                {
                    using (var writer = _fileSystem.OpenWritableFileStream("AppData/PublishSettings", fileName))
                    {
                        document.Save(writer);
                    }
                }
                else
                {
                    throw new ArgumentException("The file does not contain Subscription elements");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not load publish settings, see inner exception for details", ex);
            }           
        }

        private readonly IFileSystem _fileSystem;
    }
}