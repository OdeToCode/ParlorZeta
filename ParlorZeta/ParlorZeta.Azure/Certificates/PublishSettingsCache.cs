using System.Collections.Generic;
using System.Web;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettingsCache
    {
        private readonly PublishSettingsStore _store;

        public PublishSettingsCache(PublishSettingsStore store)
        {
            _store = store;
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
                var settings = _store.GetAllSettings();
                list.AddRange(settings);
                SetToCache(list);
            }
            return GetFromCache();
        }

        public IList<PublishSettings> GetFromCache()
        {
            return HttpRuntime.Cache[CacheKey] as IList<PublishSettings>;
        }

        public void SetToCache(IList<PublishSettings> settings)
        {
            HttpRuntime.Cache[CacheKey] = settings;
        }

        private const string CacheKey = "_publishSettings";
    }
}