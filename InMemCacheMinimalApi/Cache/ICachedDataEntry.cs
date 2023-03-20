namespace InMemCacheMinimalApi.Cache
{
    public interface ICachedDataEntry
    {
        /// <summary>
        /// If this time is over, the cache will automatically update the content
        /// </summary>
        public TimeSpan MaxCacheTime { get; }
    }
}
