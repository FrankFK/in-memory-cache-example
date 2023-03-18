namespace InMemCacheMinimalApi.Cache
{
    internal interface ICachedDataRepository
    {
        public ICachedDataEntry? GetEntry(Type typeOfICachedDataEntry);

        public string GetStateInfo(Type typeOfICachedDataEntry);

    }
}
