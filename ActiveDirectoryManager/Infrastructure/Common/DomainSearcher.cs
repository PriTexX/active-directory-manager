using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Infrastructure.Common;

public class DomainSearcher : IDomainSearcher
{
    private IActiveDirectoryConnectionFactory _connectionFactory;
    private IDomainItemFactory _domainItemFactory;
    private readonly PropertiesToLoadResolver _propertiesToLoadResolver;

    internal DomainSearcher(IDomainItemFactory domainItemFactory, IActiveDirectoryConnectionFactory connectionFactory, PropertiesToLoadResolver propertiesToLoadResolver)
    {
        _domainItemFactory = domainItemFactory;
        _connectionFactory = connectionFactory;
        _propertiesToLoadResolver = propertiesToLoadResolver;
    }

    public DomainItem? FindOne(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DomainItem?> FindAll(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        throw new NotImplementedException();
    }

    // public IEnumerable<DomainItem?> FindAllByPropertyName(IEnumerable<string> properties, string propertyName,
    //     DomainItemType type = DomainItemType.User, PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    // {
    //     throw new NotImplementedException();
    // }

    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User, int maxQueryLength = 50)
    {
        throw new NotImplementedException();
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