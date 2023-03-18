namespace InMemCacheMinimalApi.Api
{
    internal interface ICacheInfoService
    {
        public string Get(string objectType);

        public string TriggerUpdate(string objectType);
    }
}
