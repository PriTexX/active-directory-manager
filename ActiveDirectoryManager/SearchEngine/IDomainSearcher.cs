using ActiveDirectoryManager.ActiveDirectoryItem;

namespace ActiveDirectoryManager.SearchEngine;

public interface IDomainSearcher
{
    public DomainItem? FindOne(QueryFilter queryFilter, DomainItemType type = DomainItemType.User, string[]? propertiesToLoad = null);
    public IEnumerable<DomainItem?> FindAll(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        string[]? propertiesToLoad = null);
    // public IEnumerable<DomainItem?> FindAllByNames(IEnumerable<string> names, DomainItemType type = DomainItemType.User,  // TODO добавить как extension метод
    //     string[]? propertiesToLoad = null, string? path = null, int maxQueryLength = 50);
    public IEnumerable<DomainItem?> FindAllByPropertyName(IEnumerable<string> properties, string propertyName, DomainItemType type = DomainItemType.User, 
        string[]? propertiesToLoad = null, string? path = null, int maxQueryLength = 50);
    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<QueryFilter> filters, DomainItemType type = DomainItemType.User, 
        string[]? propertiesToLoad = null, string? path = null, int maxQueryLength = 50);
    public IEnumerable<GroupItem?> GetItemGroups(DomainItem item, string[]? propertiesToLoad = null);
    public IEnumerable<UserItem?> GetGroupUsers(GroupItem group, string[]? propertiesToLoad = null);

    
    public Task<DomainItem?> FindOneAsync(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        string[]? propertiesToLoad = null);
    public IAsyncEnumerable<DomainItem?> FindAllAsync(QueryFilter queryFilter, DomainItemType type = DomainItemType.User,
        string[]? propertiesToLoad = null);
    // public IAsyncEnumerable<DomainItem?> FindAllByNamesAsync(IEnumerable<string> names, DomainItemType type = DomainItemType.User, // TODO добавить как extension метод
    //     string[]? propertiesToLoad = null, string? path = null, int maxQueryLength = 50);
    public IAsyncEnumerable<DomainItem?> FindAllByPropertyNameAsync(IEnumerable<string> properties, string propertyName, DomainItemType type = DomainItemType.User, 
        string[]? propertiesToLoad = null, string? path = null, int maxQueryLength = 50);
    public IAsyncEnumerable<DomainItem?> FindAllByQueryFiltersAsync(IEnumerable<QueryFilter> filters, DomainItemType type = DomainItemType.User, 
        string[]? propertiesToLoad = null, string? path = null, int maxQueryLength = 50);
    public IAsyncEnumerable<GroupItem?> GetItemGroupsAsync(DomainItem item, string[]? propertiesToLoad = null);
    public IAsyncEnumerable<UserItem?> GetGroupUsersAsync(GroupItem group, string[]? propertiesToLoad = null);
}