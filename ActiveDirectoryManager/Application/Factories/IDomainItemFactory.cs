using System.DirectoryServices;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Application.Factories;

public interface IDomainItemFactory
{
    public DomainItem CreateInstance(DirectoryEntry dirEntry, DomainItemType type, string[] propertiesToLoad);
    public DomainItem CreateInstance(SearchResult searchResult, DomainItemType type);
}