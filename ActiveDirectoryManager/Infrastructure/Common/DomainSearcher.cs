using System.Diagnostics;
using System.DirectoryServices;
using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.Engine;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Infrastructure.Common;

public class DomainSearcher : IDomainSearcher
{
    private readonly IActiveDirectoryConnectionFactory _connectionFactory;
    private readonly IDomainItemFactory _domainItemFactory;
    private readonly DirectorySearcherBuilder _directorySearcherBuilder; 
    private readonly PropertiesToLoadResolver _propertiesToLoadResolver;

    internal DomainSearcher(IDomainItemFactory domainItemFactory, IActiveDirectoryConnectionFactory connectionFactory, PropertiesToLoadResolver propertiesToLoadResolver, DirectorySearcherBuilder directorySearcherBuilder)
    {
        _domainItemFactory = domainItemFactory;
        _connectionFactory = connectionFactory;
        _directorySearcherBuilder = directorySearcherBuilder;
        _propertiesToLoadResolver = propertiesToLoadResolver;
    }

    public DomainItem? FindOne(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type,
            searchQuery.GetQueryFilter(), _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        
        var searchResult = DomainSearcherEngine.FindOneItem(directorySearcher, type);
        
        return _domainItemFactory.CreateInstance(searchResult, type);
    }

    public IEnumerable<DomainItem?> FindAll(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type,
            searchQuery.GetQueryFilter(), _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher, type);
        
        foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item, type);
    }

    public IEnumerable<DomainItem?> FindAllByQueryFilters(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User)
    {
        foreach (var searchQuery in queries)
        {
            var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), type, searchQuery.GetQueryFilter(), 
                _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
            
            var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher, type);
            
            foreach (var item in searchResult)
                yield return _domainItemFactory.CreateInstance(item, type);
        }
    }

    public IEnumerable<GroupItem?> FindItemGroups(DomainItem domainItem, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), domainItem.DomainItemType, searchQuery!.GetQueryFilter(), 
            _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        directorySearcher.Filter =
            $"(&(objectCategory=group)(member:1.2.840.113556.1.4.1941:={domainItem.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher, DomainItemType.Group);

        foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToGroup();
    }

    public IEnumerable<UserItem?> FindGroupUsers(GroupItem group, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(_connectionFactory.Connect(), group.DomainItemType, searchQuery!.GetQueryFilter(), 
            _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        directorySearcher.Filter =
            $"(&(sAMAccountType=805306368)(memberOf:1.2.840.113556.1.4.1941:={group.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItems(directorySearcher, DomainItemType.User);

        foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToUser();
    }

    public async Task<DomainItem?> FindOneAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), type,
            searchQuery.GetQueryFilter(), _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        
        var searchResult = await DomainSearcherEngine.FindOneItemAsync(directorySearcher, type);

        return _domainItemFactory.CreateInstance(searchResult, type);
    }

    public async IAsyncEnumerable<DomainItem?> FindAllAsync(SearchQuery searchQuery, DomainItemType type = DomainItemType.User)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), type,
            searchQuery.GetQueryFilter(), _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        
        var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher, type);
        
        await foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item, type);
    }

    public async IAsyncEnumerable<DomainItem?> FindAllByQueryFiltersAsync(IEnumerable<SearchQuery> queries, DomainItemType type = DomainItemType.User)
    {
        foreach (var searchQuery in queries)
        {
            var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), type, searchQuery.GetQueryFilter(), 
                _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
            
            var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher, type);
            
            await foreach (var item in searchResult)
                yield return _domainItemFactory.CreateInstance(item, type);
        }
    }

    public async IAsyncEnumerable<GroupItem?> FindItemGroupsAsync(DomainItem domainItem, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), domainItem.DomainItemType, searchQuery!.GetQueryFilter(), 
            _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        directorySearcher.Filter =
            $"(&(objectCategory=group)(member:1.2.840.113556.1.4.1941:={domainItem.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher, DomainItemType.Group);

        await foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item, DomainItemType.Group).ToGroup();
    }

    public async IAsyncEnumerable<UserItem?> FindGroupUsersAsync(GroupItem group, SearchQuery? searchQuery = null)
    {
        var directorySearcher = _directorySearcherBuilder.CreateInstance(await _connectionFactory.ConnectAsync(), group.DomainItemType, searchQuery!.GetQueryFilter(), 
            _propertiesToLoadResolver.Resolve(searchQuery.GetPropertyLoader()));
        directorySearcher.Filter =
            $"(&(sAMAccountType=805306368)(memberOf:1.2.840.113556.1.4.1941:={group.DistinguishedName}))";
        
        var searchResult = DomainSearcherEngine.FindAllItemsAsync(directorySearcher, DomainItemType.User);

        await foreach (var item in searchResult)
            yield return _domainItemFactory.CreateInstance(item ?? throw new InvalidOperationException(), DomainItemType.Group).ToUser();
    }
}