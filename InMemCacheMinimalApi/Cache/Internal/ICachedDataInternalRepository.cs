namespace InMemCacheMinimalApi.Cache.Internal
{
    internal interface ICachedDataInternalRepository
    {
        public CachedData? Get(string key);
        public void InsertOrUpdate(string key, CachedData data);
    }
}
