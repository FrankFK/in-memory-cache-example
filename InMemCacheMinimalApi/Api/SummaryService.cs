using InMemCacheMinimalApi.BusinessLogic;
using InMemCacheMinimalApi.Cache;

namespace InMemCacheMinimalApi.Api
{
    internal sealed class SummaryService : ISummaryService
    {
        private readonly ICachedDataRepository _cachedDataRepository;

        public SummaryService(ICachedDataRepository cachedDataRepository)
        {
            _cachedDataRepository = cachedDataRepository;
        }
        public string Get(string objectType)
        {
            switch (objectType)
            {
                case "authors":
                    if (_cachedDataRepository.GetEntry(typeof(ListOfAuthors)) is ListOfAuthors listOfAuthors)
                    {
                        return $"{listOfAuthors.AuthorsCount} authors ({_cachedDataRepository.GetStateInfo(typeof(ListOfAuthors))})";
                    }
                    else
                        return $"No authors data found ({_cachedDataRepository.GetStateInfo(typeof(ListOfAuthors))})";
                case "projects":
                    if (_cachedDataRepository.GetEntry(typeof(ListOfProjects)) is ListOfProjects listOfProjects)
                    {
                        return $"{listOfProjects.ProjectsCount} projects ({_cachedDataRepository.GetStateInfo(typeof(ListOfProjects))})";
                    }
                    else
                        return $"No projects data found ({_cachedDataRepository.GetStateInfo(typeof(ListOfProjects))})";
                default:
                    throw new ArgumentOutOfRangeException(objectType);
            }
        }
    }

}
