using ActiveDirectoryManager.Core;

namespace ActiveDirectoryManager.Application;

public interface IDomainSearcher
{
    public DomainItem? FindOne(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    public IEnumerable<DomainItem?> FindAll(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    public IEnumerable<GroupItem?> FindItemGroups(DomainItem domainItem, SearchQuery? searchQuery = null);
    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, SearchQuery? searchQuery = null);

    
    public Task<DomainItem?> FindOneAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    public IAsyncEnumerable<DomainItem?> FindAllAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User);
    public IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem domainItem, SearchQuery? searchQuery = null); 
    public IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, SearchQuery? searchQuery = null);
}