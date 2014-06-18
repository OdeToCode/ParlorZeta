using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ParlorZeta.Azure.Contracts;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettingsStore
    {
        public PublishSettingsStore(IFileSystem fileSystem,  PublishSettingsCache cache, IUserSettings userSettings)
        {
            _fileSystem = fileSystem;
            _cache = cache;
            _userSettings = userSettings;
        }

        public virtual void SaveSettings(string fileName, Stream inputFileStream)
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

        public virtual void DeleteSubscriptions(string fileName)
        {
            _fileSystem.Delete(fileName);
            _cache.Empty();
        }

        public virtual IList<PublishSettings> GetAllSettings()
        {
            var settings = _cache.GetFromCache();
            if (settings == null)
            {
                var files = _fileSystem.GetFileNames(BaseDirectory);
                settings = (from file in files let document = XDocument.Load(file) select new PublishSettings(document, file)).ToList();
                _cache.SetToCache(settings);
            }
            return settings;
        }

        public virtual string BaseDirectory
        {
            get
            {
                return "App_Data/PublishSettings";        
            }
        }

        public virtual PublishSettings GetUserSelectedSettings()
        {
            var id = _userSettings.GetSelectedSubscriptionId();
            return GetSettingById(id);
        }

        public virtual PublishSettings GetSettingById(string id)
        {
            var setting = GetAllSettings().FirstOrDefault(s => s.Id == id);
            return setting;
        }

        public virtual void SetSelectedSettingsId(string id)
        {
            _userSettings.SetSelectedSubscriptionId(id);
        }

        public virtual IEnumerable<PublishSettings> GetSettingByFile(string filename)
        {
            return GetAllSettings().Where(s => s.Filename == filename);
        }

        private readonly IFileSystem _fileSystem;
        private readonly PublishSettingsCache _cache;
        private readonly IUserSettings _userSettings;
    }
}