using System.Runtime.Versioning;
using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.Engine;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Infrastructure.Common;

[SupportedOSPlatform("windows")]
public sealed class DomainSearcher : IDomainSearcher
{
    private readonly IActiveDirectoryConnectionFactory _connectionFactory;
    private readonly IDomainItemFactory _domainItemFactory;
    private readonly DirectorySearcherBuilder _directorySearcherBuilder; 
    private readonly IPropertyResolver _propertiesToLoadResolver;

    internal DomainSearcher(IDomainItemFactory domainItemFactory, IActiveDirectoryConnectionFactory connectionFactory, IPropertyResolver propertiesToLoadResolver, DirectorySearcherBuilder directorySearcherBuilder)
    {
        _domainItemFactory = domainItemFactory;
        _connectionFactory = connectionFactory;
        _directorySearcherBuilder = directorySearcherBuilder;
        _propertiesToLoadResolver = propertiesToLoadResolver;
    }

    public DomainItem? FindOne(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type,
            searchQuery.GetQueryFilter(), searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        
        var searchResult = DomainSearcherEngine.FindOneItem(directorySearcher);

        return searchResult is null ? null : _domainItemFactory.CreateInstance(searchResult, type);
    }

    public IEnumerable<DomainItem?> FindAll(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type,
            searchQuery.GetQueryFilter(), searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher);
        
        foreach (var item in searchResult)
            yield return item is null ? null : _domainItemFactory.CreateInstance(item, type);
    }

    public IEnumerable<GroupItem?> FindItemGroups(DomainItem domainItem, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), domainItem.DomainItemType, searchQuery!.GetQueryFilter(), 
            searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        directorySearcher.Filter =
            $"(&(objectCategory=group)(member:1.2.840.113556.1.4.1941:={domainItem.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher);

        foreach (var item in searchResult)
            yield return item is null ? null : _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToGroup();
    }

    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), group.DomainItemType, searchQuery!.GetQueryFilter(), 
            searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        directorySearcher.Filter =
            $"(&(sAMAccountType=805306368)(memberOf:1.2.840.113556.1.4.1941:={group.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher);

        foreach (var item in searchResult)
            yield return item is null ? null : _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToUser();
    }

    public async Task<DomainItem?> FindOneAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), type,
            searchQuery.GetQueryFilter(), searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        
        var searchResult = await DomainSearcherEngine.FindOneItemAsync(directorySearcher);

        return searchResult is null ? null : _domainItemFactory.CreateInstance(searchResult, type);
    }

    public async IAsyncEnumerable<DomainItem?> FindAllAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), type,
            searchQuery.GetQueryFilter(), searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        
        var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher);
        
        await foreach (var item in searchResult)
            yield return item is null ? null : _domainItemFactory.CreateInstance(item, type);
    }

    public async IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem domainItem, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), domainItem.DomainItemType, searchQuery!.GetQueryFilter(), 
            searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        directorySearcher.Filter =
            $"(&(objectCategory=group)(member:1.2.840.113556.1.4.1941:={domainItem.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher);

        await foreach (var item in searchResult)
            yield return item is null ? null : _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToGroup();
    }

    public async IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), group.DomainItemType, searchQuery!.GetQueryFilter(), 
            searchQuery.GetPropertiesToLoad(_propertiesToLoadResolver));
        directorySearcher.Filter =
            $"(&(sAMAccountType=805306368)(memberOf:1.2.840.113556.1.4.1941:={group.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher);

        await foreach (var item in searchResult)
            yield return item is null ? null : _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToUser();
    }
}