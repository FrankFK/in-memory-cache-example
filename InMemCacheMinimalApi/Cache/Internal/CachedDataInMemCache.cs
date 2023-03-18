namespace InMemCacheMinimalApi.Cache.Internal
{
    internal sealed class CachedDataInMemCache : ICachedDataInternalRepository
    {
        private readonly Dictionary<string, CachedData> _cachedObjects;

        public CachedDataInMemCache()
        {
            _cachedObjects = new();
        }

        public CachedData? Get(string key)
        {
            if (_cachedObjects.ContainsKey(key))
                return _cachedObjects[key];
            else
                return null;
        }

        public void InsertOrUpdate(string key, CachedData data)
        {
            if (_cachedObjects.ContainsKey(key))
            {
                _cachedObjects.Remove(key);
            }
            _cachedObjects.Add(key, data);
        }
    }
}
