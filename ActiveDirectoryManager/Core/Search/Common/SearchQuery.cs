namespace ActiveDirectoryManager.Core;

public sealed class SearchQuery
{
    internal PropertyLoader _propertyLoader = PropertyLoader.Empty();
    internal readonly QueryFilter _queryFilter = QueryFilter.Empty();

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

    internal string GetQueryFilter()
    {
        return _queryFilter.ToStringFilter();
    }

    internal PropertyLoader GetPropertyLoader()
    {
        return  _propertyLoader;
    }
}