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
            HttpRuntime.Cache[CacheKey] = null;
        }

        private const string CacheKey = "_publishSettings";
    }
}