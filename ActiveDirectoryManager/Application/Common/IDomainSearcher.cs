using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search;

namespace ActiveDirectoryManager.Application.Common;

public interface IDomainSearcher
{
    public DomainItem? FindOne(QueryFilter queryFilter, DomainItemType type = DomainItemType.User, PropertyLoader? propertiesToLoad = null);
    public IEnumerable<DomainItem?> FindAll(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null);
    // public IEnumerable<DomainItem?> FindAllByNames(IEnumerable<string> names, DomainItemType type = DomainItemType.User,  // TODO добавить как extension метод
    //     PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IEnumerable<DomainItem?> FindAllByPropertyName(IEnumerable<string> properties, string propertyName, DomainItemType type = DomainItemType.User, 
        PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<QueryFilter> filters, DomainItemType type = DomainItemType.User, 
        PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IEnumerable<GroupItem?> FindItemGroups(DomainItem item, PropertyLoader? propertiesToLoad = null);
    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, PropertyLoader? propertiesToLoad = null);

    
    public Task<DomainItem?> FindOneAsync(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null);
    public IAsyncEnumerable<DomainItem?> FindAllAsync(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        PropertyLoader? propertiesToLoad = null);
    // public IAsyncEnumerable<DomainItem?> FindAllByNamesAsync(IEnumerable<string> names, DomainItemType type = DomainItemType.User, // TODO добавить как extension метод
    //     PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IAsyncEnumerable<DomainItem?> FindAllByPropertyNameAsync(IEnumerable<string> properties, string propertyName, DomainItemType type = DomainItemType.User, 
        PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IAsyncEnumerable<DomainItem?> FindAllByQueryFiltersAsync(IEnumerable<QueryFilter> filters, DomainItemType type = DomainItemType.User, 
        PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem item, PropertyLoader? propertiesToLoad = null); 
    public IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, PropertyLoader? propertiesToLoad = null);
}