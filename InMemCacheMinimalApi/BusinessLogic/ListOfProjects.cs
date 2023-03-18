using InMemCacheMinimalApi.Cache;

namespace InMemCacheMinimalApi.BusinessLogic
{
    internal record ProjectData(string Name);

    internal sealed class ListOfProjects : ICachedDataEntry
    {
        private readonly List<ProjectData>? _projects;

        public ListOfProjects(List<ProjectData>? projects)
        {
            _projects = projects;
        }

        public TimeSpan MaxCacheTime { get { return new TimeSpan(0, 2, 0); } }

        public int ProjectsCount
        {
            get { return _projects != null ? _projects.Count : 0; }
        }
    }
}
