using ActiveDirectoryManager.ActiveDirectoryItem;
using ActiveDirectoryManager.ConnectionFactory;
using ActiveDirectoryManager.ItemFactory;

namespace ActiveDirectoryManager.SearchEngine;

public class DomainSearcher
{
    private IActiveDirectoryConnectionFactory _connectionFactory;
    private IDomainItemFactory _domainItemFactory;

    public DomainSearcher(IDomainItemFactory domainItemFactory, IActiveDirectoryConnectionFactory connectionFactory)
    {
        _domainItemFactory = domainItemFactory;
        _connectionFactory = connectionFactory;
    }

    public DomainItem? FindOne(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DomainItem?> FindAll(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DomainItem?> FindAllByPropertyName(IEnumerable<string> properties, string propertyName,
        DomainItemType type = DomainItemType.User, PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<QueryFilter> filters, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GroupItem?> FindItemGroups(DomainItem item, PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public Task<DomainItem?> FindOneAsync(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<DomainItem?> FindAllAsync(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<DomainItem?> FindAllByPropertyNameAsync(IEnumerable<string> properties, string propertyName,
        DomainItemType type = DomainItemType.User, PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<DomainItem?> FindAllByQueryFiltersAsync(IEnumerable<QueryFilter> filters, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem item, PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, PropertyLoader? propertiesToLoad = null)
    {
        throw new NotImplementedException();
    }
}