using System.DirectoryServices;
using ActiveDirectoryManager.ActiveDirectoryItem;

namespace ActiveDirectoryManager.SearchEngine;

internal interface IDomainSearcherEngine
{
    public DomainItem? FindOneItem(DirectorySearcher searcher, DomainItemType itemType);
    public IEnumerable<DomainItem?> FindAllItems(DirectorySearcher searcher, DomainItemType itemType);
    public Task<DomainItem?> FindOneItemAsync(DirectorySearcher searcher, DomainItemType itemType);
    public IAsyncEnumerable<DomainItem?> FindAllItemsAsync(DirectorySearcher searcher, DomainItemType itemType);
}