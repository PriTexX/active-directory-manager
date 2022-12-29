using System.DirectoryServices;
using ActiveDirectoryManager.ActiveDirectoryItem;
using ActiveDirectoryManager.ItemFactory;


namespace ActiveDirectoryManager.SearchEngine;

internal class DomainSearcherEngine
{
    internal IDomainItemFactory DomainItemFactory;
    
    public DomainItem? FindOneItem(DirectorySearcher searcher, DomainItemType itemType)
    {
        var item = searcher.FindOne();
        return item is null ? null : DomainItemFactory.CreateInstance(item, itemType);
    }

    public IEnumerable<DomainItem?> FindAllItems(DirectorySearcher searcher, DomainItemType itemType)
    {
        var results = searcher.FindAll(); // TODO добавить обработку исключений
        
        foreach (SearchResult result in results)
        {
            yield return DomainItemFactory.CreateInstance(result, itemType);
        }
    }
    
    public async Task<DomainItem?> FindOneItemAsync(DirectorySearcher searcher, DomainItemType itemType)
    {
        var item = await Task.Run(() => FindOneItem(searcher, itemType));
        return item;
    }
    
    public async IAsyncEnumerable<DomainItem?> FindAllItemsAsync(DirectorySearcher searcher, DomainItemType itemType)
    {
        searcher.Asynchronous = true;
        var items = await Task.Run(() => FindAllItems(searcher, itemType));

        foreach (var item in items)
        {
            yield return item;
        }
    }
}