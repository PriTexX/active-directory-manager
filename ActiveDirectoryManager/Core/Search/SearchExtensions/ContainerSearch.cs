using ActiveDirectoryManager.Application;

namespace ActiveDirectoryManager.Core;

public struct ContainerSearch
{
    private readonly IDomainSearcher _searcher;
    private readonly SearchQuery _searchQuery;

    public ContainerSearch()
    {
        throw new NotSupportedException();
    }
    
    internal ContainerSearch(IDomainSearcher searcher)
    {
        _searcher = searcher;
        _searchQuery = SearchQuery.Create();
    }
    
    public ContainerSearch WithObjectName(string name)
    {
        _searchQuery._queryFilter.Name = new List<string>{name};
        return this;
    }
    
    public ContainerSearch WithLoadedProperties(SearchQuery propertiesToLoad)
    {
        _searchQuery._propertyLoader = propertiesToLoad.GetPropertyLoader();
        return this;
    }
    
    public async Task<ContainerItem?> FindContainerAsync()
    {
        return (await _searcher.FindOneAsync(_searchQuery, DomainItemType.Container))?.AsContainer();
    }
    
    public ContainerItem? FindContainer()
    {
        return _searcher.FindOne(_searchQuery, DomainItemType.Container)?.AsContainer();
    }
}