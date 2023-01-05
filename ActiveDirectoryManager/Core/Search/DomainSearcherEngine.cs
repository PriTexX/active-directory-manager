using System.DirectoryServices;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Core.Search;

internal class DomainSearcherEngine
{
    internal IDomainItemFactory DomainItemFactory;
    
    public SearchResult? FindOneItem(DirectorySearcher searcher, DomainItemType itemType)
    {
        return searcher.FindOne();
    }

    public IEnumerable<SearchResult?> FindAllItems(DirectorySearcher searcher, DomainItemType itemType)
    {
        return searcher.FindAll().Cast<SearchResult>();
    }
    
    public async Task<SearchResult?> FindOneItemAsync(DirectorySearcher searcher, DomainItemType itemType)
    {
        return await Task.Run(() => FindOneItem(searcher, itemType)).ConfigureAwait(false);
    }
    
    public async IAsyncEnumerable<SearchResult?> FindAllItemsAsync(DirectorySearcher searcher, DomainItemType itemType)
    {
        searcher.Asynchronous = true;
        foreach (var item in await Task.Run(() => FindAllItems(searcher, itemType)))
        {
            yield return item;
        }
    }
}