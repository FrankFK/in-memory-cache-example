using InMemCacheMinimalApi.Cache.Internal;

namespace InMemCacheMinimalApi.Cache
{
    public static class CachedDataConfiguration
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddHostedService<CachedDataUpdateService>();
            services.AddSingleton<ICachedDataRepository, CachedDataRepository>();
            services.AddSingleton<ICachedDataUpdater, CachedDataUpdater>();
            services.AddSingleton<ICachedDataInternalRepository, CachedDataInMemCache>();

        }
    }
}
