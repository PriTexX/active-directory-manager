using System.DirectoryServices;
using ActiveDirectoryManager.ActiveDirectoryItem;

namespace ActiveDirectoryManager.ItemFactory;

public interface IDomainItemFactory
{
    public DomainItem CreateInstance(DirectoryEntry dirEntry, DomainItemType type, string[] propertiesToLoad);
    public DomainItem CreateInstance(SearchResult searchResult, DomainItemType type);
}