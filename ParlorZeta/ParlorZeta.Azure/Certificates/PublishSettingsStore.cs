using System;
using System.Collections.Generic;
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
                    using (var writer = _fileSystem.OpenWritableFileStream(BaseDirectory, fileName))
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

        public void DeleteSubscriptions(string fileName)
        {
            _fileSystem.Delete(fileName);
        }

        public string BaseDirectory
        {
            get
            {
                return "AppData/PublishSettings";        
            }
        }

        public IEnumerable<PublishSettings> GetAllSettings()
        {
            var files = _fileSystem.GetFileNames(BaseDirectory);
            foreach (var file in files)
            {
                var document = XDocument.Load(file);
                var subscriptions = document.Descendants("Subscription");
                foreach (var subscription in subscriptions)
                {
                    var settings = new PublishSettings(subscription, file);
                    yield return settings;
                }
            }
        }

        private readonly IFileSystem _fileSystem;
    }
}