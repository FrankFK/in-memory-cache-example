using InMemCacheMinimalApi.Cache;

namespace InMemCacheMinimalApi.BusinessLogic
{
    internal class ListOfAuthorsProducer : ICachedDataEntryProducer
    {
        private readonly ILogger<ListOfAuthorsProducer> _logger;

        public ListOfAuthorsProducer(ILogger<ListOfAuthorsProducer> logger)
        {
            _logger = logger;
        }

        public Type GeneratesDataType { get { return typeof(ListOfAuthors); } }

        public async Task<ICachedDataEntry> GenerateDataAsync()
        {
            ListOfAuthors result;
            // This is the code that has a long runtime
            using (_logger.BeginScope("Loading authors"))
            {
                _logger.LogInformation("Start loading authors");
                await Task.Delay(15 * 1000); // Simulate  a time here
                result = new ListOfAuthors(new List<AuthorData> { new AuthorData("Stanislaw", "Lem"), new AuthorData("Arthur C.", "Clark") });
                _logger.LogInformation("Loading authors is finished");
            }
            return result;
        }
    }
}
