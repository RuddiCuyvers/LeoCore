using System;
using System.Runtime.Caching;
using WGK.Lib.Ioc;

namespace WGK.Lib.CacheData
{
    public static class CacheManager
    {
        private static ObjectCache iCache;

        static CacheManager()
        {
            //ICacheObject vCacheObject = IocManager.Resolve<ICacheObject>(); 
            iCache = IocManager.Resolve<ObjectCache>();
        }

        public static object Get(string pKey)
        {
            return iCache[pKey];
        }

        public static void Set(string pKey, object pData, int pCacheTimeInMinutes)
        {
            CacheItemPolicy vPolicy = new CacheItemPolicy();
            vPolicy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(pCacheTimeInMinutes);

            if (iCache.Contains(pKey))
            {
                iCache.Remove(pKey);
            }

            iCache.Add(new CacheItem(pKey, pData), vPolicy);
        }

        public static bool Contains(string pKey)
        {
            return (iCache[pKey] != null);
        }

        public static void Remove(string pKey)
        {
            iCache.Remove(pKey);
        }
    }

}
