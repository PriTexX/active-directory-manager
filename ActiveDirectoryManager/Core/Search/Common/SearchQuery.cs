using ActiveDirectoryManager.Core.Search.Filter;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Core.Search.Common;

public sealed class SearchQuery
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

    internal string GetQueryFilter()
    {
        return _queryFilter.ToStringFilter();
    }

    internal string[] GetPropertiesToLoad(IPropertyResolver resolver)
    {
        return  resolver.Resolve(_propertyLoader);
    }
}