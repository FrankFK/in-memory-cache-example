namespace InMemCacheMinimalApi.Cache.Internal
{
    internal sealed class CachedData
    {
        public enum CacheState
        {
            OK,
            ShouldBeUpdated,
            InUpdate
        }

        private readonly DateTime _creationTime = DateTime.MinValue;

        public CachedData(ICachedDataEntry? entry)
        {
            Entry = entry;
            if (Entry != null)
            {
                State = CacheState.OK;
                _creationTime = DateTime.Now;
            }
            else
            {
                State = CacheState.ShouldBeUpdated;
                _creationTime = DateTime.MinValue;
            }
        }

        public ICachedDataEntry? Entry { get; }

        public CacheState State { get; set; }

        public string StateInfo
        {
            get
            {
                string result;
                if (Entry != null)
                {
                    result = State switch
                    {
                        CacheState.OK => $"Cached data, {Age} old, auto update in {AutoRefresh}",
                        CacheState.ShouldBeUpdated => $"Cached data, {Age} hours old, but will be updated soon",
                        CacheState.InUpdate => $"Cached data, {Age} hours old, update is running",
                        _ => "Unexpceted state",
                    };
                }
                else
                {
                    result = "Initializing, please come back in a few minutes";
                }
                return result;
            }
        }

        public TimeSpan AutoRefresh
        {
            get
            {
                if (Age == TimeSpan.Zero)
                    return TimeSpan.Zero;
                else if (Entry == null)
                    return TimeSpan.Zero;
                else
                    return Entry.MaxCacheTime - Age;
            }
        }

        private TimeSpan Age
        {
            get
            {
                if (_creationTime == DateTime.MinValue)
                    return TimeSpan.Zero;
                else
                {
                    var diff = DateTime.Now - _creationTime;
                    return diff;
                }
            }
        }


    }
}
