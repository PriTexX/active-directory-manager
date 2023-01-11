using System.DirectoryServices;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Core.Search.Engine;

internal class DomainSearcherEngine
{
    public static SearchResult? FindOneItem(DirectorySearcher searcher, DomainItemType itemType)
    {
        return searcher.FindOne();
    }

    public static IEnumerable<SearchResult?> FindAllItems(DirectorySearcher searcher, DomainItemType itemType)
    {
        return searcher.FindAll().Cast<SearchResult>();
    }
    
    public static async Task<SearchResult?> FindOneItemAsync(DirectorySearcher searcher, DomainItemType itemType)
    {
        return await Task.Run(() => FindOneItem(searcher, itemType)).ConfigureAwait(false);
    }
    
    public static async IAsyncEnumerable<SearchResult?> FindAllItemsAsync(DirectorySearcher searcher, DomainItemType itemType)
    {
        searcher.Asynchronous = true;
        foreach (var item in await Task.Run(() => FindAllItems(searcher, itemType)))
        {
            yield return item;
        }
    }
}