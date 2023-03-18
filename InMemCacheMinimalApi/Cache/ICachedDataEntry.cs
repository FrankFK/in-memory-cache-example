namespace InMemCacheMinimalApi.Cache
{
    internal interface ICachedDataEntry
    {
        /// <summary>
        /// If this time is over, the cache will automatically update the content
        /// </summary>
        public TimeSpan MaxCacheTime { get; }
    }
}
