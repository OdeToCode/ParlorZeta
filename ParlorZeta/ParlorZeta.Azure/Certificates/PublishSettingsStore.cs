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
            //try
            //{
            //    var settings = new PublishSettings(inputFileStream);
            //}

            try
            {
                var document = XDocument.Load(inputFileStream);
                if (document.Descendants("Subscriptions").Any())
                {
                    //using (var writer = _fileSystem.OpenForWrite("AppData/PublishSettings", fileName))
                    //{
                    //    document.Save(writer);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not load publish settings, is it correct file?", ex);
            }           
        }

        private readonly IFileSystem _fileSystem;
    }
}