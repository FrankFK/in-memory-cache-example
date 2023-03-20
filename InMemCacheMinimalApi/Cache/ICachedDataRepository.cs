namespace InMemCacheMinimalApi.Cache
{
    public interface ICachedDataRepository
    {
        public ICachedDataEntry? GetEntry(Type typeOfICachedDataEntry);

        public string GetStateInfo(Type typeOfICachedDataEntry);

    }
}
