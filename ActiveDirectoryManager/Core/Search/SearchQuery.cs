namespace ActiveDirectoryManager.Core.Search;

public class SearchQuery
{
    private readonly PropertyLoader _propertyLoader = PropertyLoader.Empty();
    private readonly QueryFilter _queryFilter = QueryFilter.Empty();

    public static SearchQuery Create()
    {
        return new SearchQuery();
    }

    public SearchQuery ConfigurePropertiesToLoad(Func<PropertyLoaderConfigurator, PropertyLoaderConfigurator> propertyLoaderConfigurator)
    {
        var configurator = PropertyLoaderConfigurator.Create(_propertyLoader);
        propertyLoaderConfigurator(configurator);
        return this;
    }

    public SearchQuery ConfigureQueryFilter(Func<QueryFilterConfigurator, QueryFilterConfigurator> queryFilterConfigurator)
    {
        var configurator = QueryFilterConfigurator.Create(_queryFilter);
        queryFilterConfigurator(configurator);
        return this;
    }

    public string GetStringFilter()
    {
        return _queryFilter.ToStringFilter();
    }

    public string[] GetPropertiesToLoad()
    {
        return  PropertiesToLoadResolver.Resolve(_propertyLoader);
    }
}