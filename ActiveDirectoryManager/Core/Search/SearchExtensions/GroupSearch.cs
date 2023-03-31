using ActiveDirectoryManager.Application;

namespace ActiveDirectoryManager.Core;

public struct GroupSearch
{
    private readonly IDomainSearcher _searcher;
    private readonly SearchQuery _searchQuery;

    public GroupSearch()
    {
        throw new NotSupportedException();
    }
    
    internal GroupSearch(IDomainSearcher searcher)
    {
        _searcher = searcher;
        _searchQuery = SearchQuery.Create();
    }
    
    public GroupSearch WithObjectName(string name)
    {
        _searchQuery._queryFilter.Name = new List<string>{name};
        return this;
    }
    
    public GroupSearch WithLoadedProperties(SearchQuery propertiesToLoad)
    {
        _searchQuery._propertyLoader = propertiesToLoad.GetPropertyLoader();
        return this;
    }
    
    public async Task<GroupItem?> FindSecurityGroupAsync()
    {
        return (await _searcher.FindOneAsync(_searchQuery, DomainItemType.Group))?.AsGroup();
    }
    
    public GroupItem? FindSecurityGroup()
    {
        return _searcher.FindOne(_searchQuery, DomainItemType.Group)?.AsGroup();
    }
}