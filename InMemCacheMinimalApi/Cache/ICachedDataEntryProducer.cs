namespace InMemCacheMinimalApi.Cache
{
    /// <summary>
    /// This interface makes available all the methods to create a cached object that are necessary for caching
    /// </summary>
    internal interface ICachedDataEntryProducer
    {
        /// <summary>
        /// The producer generates ICachedDataEntry objects, this property is the typeof this object.
        /// </summary>
        public Type GeneratesDataType { get; }

        public Task<ICachedDataEntry> GenerateDataAsync();

    }
}
