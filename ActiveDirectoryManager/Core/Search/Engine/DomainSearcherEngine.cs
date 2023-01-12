using System.DirectoryServices;
using System.Runtime.Versioning;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Core.Search.Engine;

[SupportedOSPlatform("windows")]
internal static class DomainSearcherEngine
{
    public static SearchResult? FindOneItem(DirectorySearcher searcher)
    {
        return searcher.FindOne();
    }

    public static IEnumerable<SearchResult?> FindAllItems(DirectorySearcher searcher)
    {
        return searcher.FindAll().Cast<SearchResult>();
    }
    
    public static async Task<SearchResult?> FindOneItemAsync(DirectorySearcher searcher)
    {
        return await Task.Run(() => FindOneItem(searcher)).ConfigureAwait(false);
    }
    
    public static async IAsyncEnumerable<SearchResult?> FindAllItemsAsync(DirectorySearcher searcher)
    {
        searcher.Asynchronous = true;
        foreach (var item in await Task.Run(() => FindAllItems(searcher)))
        {
            yield return item;
        }
    }
}