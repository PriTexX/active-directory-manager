using ActiveDirectoryManager.Core.Search.Filter;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Core.Search.Common;

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

    internal QueryFilter GetQueryFilter()
    {
        return _queryFilter;
    }

    internal PropertyLoader GetPropertyLoader()
    {
        return  _propertyLoader;
    }
}