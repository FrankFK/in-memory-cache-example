using System.Diagnostics;

namespace InMemCacheMinimalApi.Cache.Internal
{
    internal class CachedDataUpdateService : BackgroundService
    {
        private readonly ICachedDataUpdater _cachedDataUpdater;
        private readonly ILogger<CachedDataUpdateService> _logger;

        public CachedDataUpdateService(ICachedDataUpdater cachedDataUpdater, ILogger<CachedDataUpdateService> logger)
        {
            _cachedDataUpdater = cachedDataUpdater;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Checking if there is data to be updated.");
                await _cachedDataUpdater.RunNeededUpdatesAsync();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
