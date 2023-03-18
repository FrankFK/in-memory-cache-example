using InMemCacheMinimalApi.Cache;

namespace InMemCacheMinimalApi.BusinessLogic
{
    internal class ListOfProjectsProducer : ICachedDataEntryProducer
    {
        private readonly ILogger<ListOfProjectsProducer> _logger;

        public ListOfProjectsProducer(ILogger<ListOfProjectsProducer> logger)
        {
            _logger = logger;
        }

        public Type GeneratesDataType { get { return typeof(ListOfProjects); } }

        public async Task<ICachedDataEntry> GenerateDataAsync()
        {
            ListOfProjects result;
            // This is the code that has a long runtime
            using (_logger.BeginScope("Loading projects"))
            {
                _logger.LogInformation("Start loading projects");
                await Task.Delay(15 * 1000); // Simulate  a time here
                result = new ListOfProjects(new List<ProjectData> { new ProjectData("Eden"), new ProjectData("Solaris"), new ProjectData("Transfer"), new ProjectData("The City and the Stars"), new ProjectData("2001: A Space Odyssey") });
                _logger.LogInformation("Loading projects is finished");
            }
            return result;
        }
    }
}
