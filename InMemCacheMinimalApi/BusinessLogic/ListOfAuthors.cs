using InMemCacheMinimalApi.Cache;

namespace InMemCacheMinimalApi.BusinessLogic
{
    internal record AuthorData(string FirstName, string LastName);

internal sealed class ListOfAuthors : ICachedDataEntry
{
    private readonly List<AuthorData>? _authors;

    public ListOfAuthors(List<AuthorData>? authors)
    {
        _authors = authors;
    }

    public TimeSpan MaxCacheTime { get { return new TimeSpan(0, 2, 0); } }

    public int AuthorsCount
    {
        get { return _authors != null ? _authors.Count : 0; }
    }
}
}
