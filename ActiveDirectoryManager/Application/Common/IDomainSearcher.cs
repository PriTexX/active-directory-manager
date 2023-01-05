using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Common;

namespace ActiveDirectoryManager.Application.Common;

public interface IDomainSearcher
{
    public DomainItem? FindOne(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    public IEnumerable<DomainItem?> FindAll(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    // public IEnumerable<DomainItem?> FindAllByNames(IEnumerable<string> names, DomainItemType type = DomainItemType.User,  // TODO добавить как extension метод
    //     PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    // public IEnumerable<DomainItem?> FindAllByPropertyName(IEnumerable<string> properties, string propertyName, DomainItemType type = DomainItemType.User, 
    //     PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);  // TODO Добавить возможность в SearchQuery указывать множественные параметры поиска
    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User, int maxQueryLength = 50);
    public IEnumerable<GroupItem?> FindItemGroups(DomainItem item, SearchQuery? propertiesToLoad = null);
    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, SearchQuery? propertiesToLoad = null);

    
    public Task<DomainItem?> FindOneAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    public IAsyncEnumerable<DomainItem?> FindAllAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    // public IAsyncEnumerable<DomainItem?> FindAllByNamesAsync(IEnumerable<string> names, DomainItemType type = DomainItemType.User, // TODO добавить как extension метод
    //     PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    // public IAsyncEnumerable<DomainItem?> FindAllByPropertyNameAsync(IEnumerable<string> properties, string propertyName, DomainItemType type = DomainItemType.User, 
    //     PropertyLoader? propertiesToLoad = null, int maxQueryLength = 50);
    public IAsyncEnumerable<DomainItem?> FindAllByQueryFiltersAsync(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User, int maxQueryLength = 50);
    public IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem item, SearchQuery? propertiesToLoad = null); 
    public IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, SearchQuery? propertiesToLoad = null);
}