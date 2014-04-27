using System.Collections.Generic;
using System.Web;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettingsCache
    {
        public IList<PublishSettings> GetFromCache()
        {
            return HttpRuntime.Cache[CacheKey] as IList<PublishSettings>;
        }

        public void SetToCache(IList<PublishSettings> settings)
        {
            HttpRuntime.Cache[CacheKey] = settings;
        }

        public void Empty()
        {
            if (HttpRuntime.Cache[CacheKey] != null)
            {
                HttpRuntime.Cache.Remove(CacheKey);
            }
        }

        private const string CacheKey = "_publishSettings";
    }
}