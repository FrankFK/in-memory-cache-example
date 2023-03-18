namespace InMemCacheMinimalApi.Cache.Internal
{
    internal sealed class CachedDataRepository : ICachedDataRepository
    {
        private readonly ICachedDataInternalRepository _internalRepository;

        public CachedDataRepository(ICachedDataInternalRepository internalRepository)
        {
            _internalRepository = internalRepository;
        }

        public ICachedDataEntry? GetEntry(Type typeOfICachedDataEntry)
        {
            return _internalRepository.Get(typeOfICachedDataEntry.ToString())?.Entry;
        }

        public string GetStateInfo(Type typeOfICachedDataEntry)
        {
            string dataEntryKey = typeOfICachedDataEntry.ToString();
            CachedData? data = _internalRepository.Get(dataEntryKey);
            if (data != null)
                return data.StateInfo;
            else
                return $"{dataEntryKey} not found in cache!";
        }
    }
}
