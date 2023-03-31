using ActiveDirectoryManager.Application;

namespace ActiveDirectoryManager.Core;

public struct UserSearch
{
    private readonly IDomainSearcher _searcher;
    private readonly SearchQuery _searchQuery;

    public UserSearch()
    {
        throw new NotSupportedException();
    }
    
    internal UserSearch(IDomainSearcher searcher)
    {
        _searcher = searcher;
        _searchQuery = SearchQuery.Create();
    }

    public UserSearch WithSamAccountName(string samAccountName)
    {
        _searchQuery._queryFilter.SamAccountName = new List<string>{samAccountName};
        return this;
    }

    public UserSearch WithEmployeeNumber(string employeeNumber)
    {
        _searchQuery._queryFilter.EmployeeNumber = new List<string>{employeeNumber};
        return this;
    }
    
    public UserSearch WithObjectName(string name)
    {
        _searchQuery._queryFilter.Name = new List<string>{name};
        return this;
    }
    
    public UserSearch WithUserPrincipalName(string principalName)
    {
        _searchQuery._queryFilter.UserPrincipalName = new List<string>{principalName};
        return this;
    }
    
    public UserSearch WithLoadedProperties(SearchQuery propertiesToLoad)
    {
        _searchQuery._propertyLoader = propertiesToLoad.GetPropertyLoader();
        return this;
    }
    
    public UserSearch WithLoadedProperties(IEnumerable<string> propertiesToLoad)
    {
        _searchQuery.ConfigurePropertiesToLoad(loader => loader.LoadProperties(propertiesToLoad));
        return this;
    }

    public async Task<UserItem?> FindUserAsync()
    {
        return (await _searcher.FindOneAsync(_searchQuery, DomainItemType.User))?.AsUser();
    }
    
    public UserItem? FindUser()
    {
        return _searcher.FindOne(_searchQuery, DomainItemType.User)?.AsUser();
    }
}