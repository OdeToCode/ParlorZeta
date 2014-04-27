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
        public PublishSettingsStore(IFileSystem fileSystem, PublishSettingsCache cache)
        {
            _fileSystem = fileSystem;
            _cache = cache;
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
                    _cache.Empty();
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
            _cache.Empty();
        }

        public IEnumerable<PublishSettings> GetAllSettings()
        {
            var settings = _cache.GetFromCache();
            if (settings == null)
            {
                settings = new List<PublishSettings>();
                var files = _fileSystem.GetFileNames(BaseDirectory);
                foreach (var file in files)
                {
                    var document = XDocument.Load(file);
                    var subscriptions = document.Descendants("Subscription");
                    foreach (var subscription in subscriptions)
                    {
                        settings.Add(new PublishSettings(subscription, file));
                    }
                }
                _cache.SetToCache(settings);
            }
            return settings;
        }

        public string BaseDirectory
        {
            get
            {
                return "AppData/PublishSettings";        
            }
        }

        public PublishSettings GetSettingById(string id)
        {
            var setting = GetAllSettings().FirstOrDefault(s => s.Id == id);
            if (setting == null)
            {
                throw new ArgumentException("Could not find settings for " + id);
            }
            return setting;
        }

        public object GetSettingByFile(string filename)
        {
            return GetAllSettings().Where(s => s.Filename == filename);
        }

        private readonly IFileSystem _fileSystem;

        private readonly PublishSettingsCache _cache;
    }
}