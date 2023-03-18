namespace InMemCacheMinimalApi.Cache
{
    /// <summary>
    /// From the class that implements this interface, all cache objects are created and updated.
    /// </summary>
    internal interface ICachedDataUpdater
    {
        public Task RunNeededUpdatesAsync();

        /// <summary>
        /// Trigger an update of the cached object with the given type. This method return immediately, the update itself is done in a
        /// seperate background thread.
        /// </summary>
        /// <param name="typeOfICachedDataEntry">The typeof the cached object that should be updated</param>
        public void TriggerUpdate(Type typeOfICachedDataEntry);
    }
}
