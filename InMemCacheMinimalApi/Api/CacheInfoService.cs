using InMemCacheMinimalApi.BusinessLogic;
using InMemCacheMinimalApi.Cache;

namespace InMemCacheMinimalApi.Api
{
    internal class CacheInfoService : ICacheInfoService
    {
        private readonly ICachedDataRepository _cachedDataRepository;
        private readonly ICachedDataUpdater _cachedDataUpdater;

        public CacheInfoService(ICachedDataRepository cachedDataRepository, ICachedDataUpdater cachedDataUpdater)
        {
            _cachedDataRepository = cachedDataRepository;
            _cachedDataUpdater = cachedDataUpdater;
        }
        public string Get(string objectType)
        {
            return objectType switch
            {
                "authors" => _cachedDataRepository.GetStateInfo(typeof(ListOfAuthors)),
                _ => throw new ArgumentOutOfRangeException(objectType),
            };
        }

        public string TriggerUpdate(string objectType)
        {
            switch (objectType)
            {
                case "authors":
                    _cachedDataUpdater.TriggerUpdate(typeof(ListOfAuthors));
                    return "Update is triggered.";
                case "projects":
                    _cachedDataUpdater.TriggerUpdate(typeof(ListOfProjects));
                    return "Update is triggered.";
                default:
                    throw new ArgumentOutOfRangeException(objectType);
            }
        }
    }
}
