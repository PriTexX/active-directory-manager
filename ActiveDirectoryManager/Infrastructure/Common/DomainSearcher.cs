using System.Diagnostics;
using System.DirectoryServices;
using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.Engine;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Infrastructure.Common;

public class DomainSearcher : IDomainSearcher
{
    private IActiveDirectoryConnectionFactory _connectionFactory;
    private IDomainItemFactory _domainItemFactory;
    private DirectorySearcherBuilder _directorySearcherBuilder; 
    private readonly PropertiesToLoadResolver _propertiesToLoadResolver;

    internal DomainSearcher(IDomainItemFactory domainItemFactory, IActiveDirectoryConnectionFactory connectionFactory, PropertiesToLoadResolver propertiesToLoadResolver, DirectorySearcherBuilder directorySearcherBuilder)
    {
        _domainItemFactory = domainItemFactory;
        _connectionFactory = connectionFactory;
        _directorySearcherBuilder = directorySearcherBuilder;
        _propertiesToLoadResolver = propertiesToLoadResolver;
    }

    public DomainItem? FindOne(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type,
            searchQuery.GetQueryFilter(), _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        
        var searchResult = DomainSearcherEngine.FindAllItem(directorySearcher, type);
        
        return _domainItemFactory.CreateInstance(searchResult ?? throw new InvalidOperationException(), type);
    }

    public IEnumerable<DomainItem?> FindAll(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type,
            searchQuery.GetQueryFilter(), _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher, type);
        
        foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item ?? throw new InvalidOperationException(), type);
    }

    // public IEnumerable<DomainItem?> FindAllByPropertyName(IEnumerable<string> properties, string propertyName,
    //     DomainItemType type = DomainItemType.User, PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    // {
    //     throw new NotImplementedException();
    // }

    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User, int maxQueryLength = 50)
    {
        foreach (var searchQuery in queries)
        {
            var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type, searchQuery.GetQueryFilter(), 
                _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
            
            var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher, type);
            
            foreach (var item in searchResult)
                yield return _domainItemFactory.CreateInstance(item ?? throw new InvalidOperationException(), type);
        }
    }

    public IEnumerable<GroupItem?> FindItemGroups(DomainItem item, SearchQuery? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, SearchQuery? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public Task<DomainItem?> FindOneAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<DomainItem?> FindAllAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        throw new NotImplementedException();
    }

    // public IAsyncEnumerable<DomainItem?> FindAllByPropertyNameAsync(IEnumerable<string> properties, string propertyName,
    //     DomainItemType type = DomainItemType.User, PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    // {
    //     throw new NotImplementedException();
    // }

    public IAsyncEnumerable<DomainItem?> FindAllByQueryFiltersAsync(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User, int maxQueryLength = 50)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem item, SearchQuery? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, SearchQuery? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }
}