using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;
using ParlorZeta.Azure.FileSystem;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettingsCache
    {
        private readonly IFileSystem _fileSystem;

        public PublishSettingsCache(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Refresh()
        {
            GetSettings(hardRefresh: true);
        }

        public IList<PublishSettings> GetSettings(bool hardRefresh = false)
        {
            if (GetFromCache() == null || hardRefresh)
            {
                var list = new List<PublishSettings>();
                var files = _fileSystem.GetFileNames("AppData/PublishSettings");
                foreach (var file in files)
                {
                    var document = XDocument.Load(file);
                    var subscriptions = document.Descendants("Subscription");
                    foreach (var subscription in subscriptions)
                    {
                        var settings = new PublishSettings(subscription);
                        list.Add(settings);
                    }
                }
                SetToCache(list);
            }
            return GetFromCache();
        }

        public IList<PublishSettings> GetFromCache()
        {
            return HttpRuntime.Cache[Key] as IList<PublishSettings>;
        }

        public void SetToCache(IList<PublishSettings> settings)
        {
            HttpRuntime.Cache[Key] = settings;
        }

        private const string Key = "_publishSettings";
    }
}