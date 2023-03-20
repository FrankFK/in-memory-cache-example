// using static InMemCacheMinimalApi.Cache.CachedData;

namespace InMemCacheMinimalApi.Cache.Internal
{
    internal class CachedDataUpdater : ICachedDataUpdater
    {
        private readonly ICachedDataInternalRepository _cachedDataRepository;
        private readonly Dictionary<string, ICachedDataEntryProducer> _producers = new(); // All producers indexed by the Type of ICachedDataEntry they are producing
        private readonly ILogger<CachedDataUpdater> _logger;

        public CachedDataUpdater(ICachedDataInternalRepository cachedDataRepository, IEnumerable<ICachedDataEntryProducer> producers, ILogger<CachedDataUpdater> logger)
        {
            _cachedDataRepository = cachedDataRepository;
            _logger = logger;

            foreach (var producer in producers)
            {
                _producers.Add(producer.GeneratesDataType.ToString(), producer);
            }
        }

        public async Task RunNeededUpdatesAsync()
        {
            foreach (var dictEntry in _producers)
            {
                await UpdateIfNeededAsync(dictEntry.Value);
            }
        }

        public void TriggerUpdate(Type typeOfICachedDataEntry)
        {
            _logger.LogInformation("Trigger update of {CacheKeyType}", typeOfICachedDataEntry.ToString());
            string key = typeOfICachedDataEntry.ToString();

            // The update is not done directly (because it may take a long time). 
            // Instead of this the state is set to "ShouldBeUpdated" and the update is done automatically by the BackgroundService 
            lock (_cachedDataRepository)
            {
                CachedData? cachedData = _cachedDataRepository.Get(key);
                if (cachedData != null)
                {
                    cachedData.State = CachedData.CacheState.ShouldBeUpdated;
                    _cachedDataRepository.InsertOrUpdate(key, cachedData);
                }
                else
                {
                    var emptyData = new CachedData(null)
                    {
                        State = CachedData.CacheState.ShouldBeUpdated
                    };
                    _cachedDataRepository.InsertOrUpdate(key, emptyData);
                }
            }
        }


        private async Task UpdateIfNeededAsync(ICachedDataEntryProducer producer)
        {
            string typeOfICachedDataEntry = producer.GeneratesDataType.ToString();
            bool doUpdate = false;
            lock (_cachedDataRepository)
            {
                CachedData? cachedData = _cachedDataRepository.Get(typeOfICachedDataEntry);
                if (cachedData == null || cachedData.State == CachedData.CacheState.ShouldBeUpdated || cachedData.AutoRefresh < TimeSpan.Zero)
                {
                    cachedData ??= new CachedData(null);
                    cachedData.State = CachedData.CacheState.InUpdate;
                    _cachedDataRepository.InsertOrUpdate(typeOfICachedDataEntry, cachedData);
                    doUpdate = true;
                }
            }
            if (doUpdate)
            {
                _logger.LogInformation("Generation of {ICachedDataEntry} is started.", typeOfICachedDataEntry);
                ICachedDataEntry newDataEntry = await producer.GenerateDataAsync();
                if (newDataEntry.GetType().ToString() != typeOfICachedDataEntry)
                    throw new Exception("Producer returns object of wrong type");
                _logger.LogInformation("Generation of {ICachedDataEntry} is finished.", typeOfICachedDataEntry);
                lock (_cachedDataRepository)
                {
                    var newData = new CachedData(newDataEntry)
                    {
                        State = CachedData.CacheState.OK
                    };
                    _cachedDataRepository.InsertOrUpdate(typeOfICachedDataEntry, newData);
                }
            }
        }

    }
}
