using System.DirectoryServices;
using System.Reflection.Metadata.Ecma335;
using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.DirEntry;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.Engine;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;

namespace ActiveDirectoryManager.Infrastructure.Common;

public sealed class ActiveDirectoryManager : IActiveDirectoryManager // TODO: Сделать возможность делать ретрай операций
{
    private readonly IActiveDirectoryConnectionFactory _connectionFactory;
    private readonly IPropertyResolver _propertiesToLoadResolver;
    private readonly IDomainItemFactory _domainItemFactory;
    private readonly DirectoryEntryBuilder _directoryEntryBuilder = new DirectoryEntryBuilder();

    internal ActiveDirectoryManager(IActiveDirectoryConnectionFactory connectionFactory, IDomainItemFactory domainItemFactory, IPropertyResolver propertiesToLoadResolver)
    {
        _connectionFactory = connectionFactory;
        _domainItemFactory = domainItemFactory;
        _propertiesToLoadResolver = propertiesToLoadResolver;
    }

    public async Task AddToGroupAsync(DomainItem item, GroupItem groupItem)
    {
        await Task.Run(() => AddToGroup(item, groupItem));
    }
    
    public void AddToGroup(DomainItem item, GroupItem groupItem)
    {
        try
        {
            groupItem.GetUnderlyingObject().Properties["member"].Add(item);
            groupItem.Save();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task RemoveFromGroupAsync(DomainItem item, GroupItem groupItem)
    {
        await Task.Run(() => RemoveFromGroup(item, groupItem));
    }
    
    public void RemoveFromGroup(DomainItem item, GroupItem groupItem)
    {
        try
        {
            groupItem.GetUnderlyingObject().Properties["member"].Remove(item);
            groupItem.Save();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task RenameAsync(DomainItem item, string newName)
    {
        await Task.Run(() => Rename(item, newName));
    }
    
    public void Rename(DomainItem item, string newName)
    {
        try
        {
            item.GetUnderlyingObject().Rename("CN=" + newName);
            item.GetUnderlyingObject().Rename("CN=" + newName);
            item.GetUnderlyingObject().RefreshCache(new []{"distinguishedname"});
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task MoveToAsync(DomainItem item, ContainerItem containerItem)
    {
        await Task.Run(() => MoveTo(item, containerItem));
    }
    
    public void MoveTo(DomainItem item, ContainerItem containerItem)
    {
        try
        {
            item.GetUnderlyingObject().MoveTo(containerItem.GetUnderlyingObject(), item.GetUnderlyingObject().Name);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task CopyToAsync(DomainItem item, ContainerItem containerItem)
    {
        await Task.Run(() => CopyTo(item, containerItem));
    }
    
    public void CopyTo(DomainItem item, ContainerItem containerItem)
    {
        try
        {
            item.GetUnderlyingObject().CopyTo(containerItem.GetUnderlyingObject());
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    public async Task<UserItem> CreateUserAsync(ContainerItem directory, string name, string userPassword, SearchQuery propsToLoad = null)
    {
        var user = await Task.Run(() => CreateUser(directory, name, userPassword, propsToLoad));
        return user;
    }
    
    public UserItem CreateUser(ContainerItem directory, string name, string userPassword, SearchQuery propsToLoad = null)
    {
        var newUser = _directoryEntryBuilder.SetType(DirectoryEntryType.user).SetName(directory, name)
            .SetProperty("userAccountControl", userPassword).SetPassword(userPassword).Build();

        return _domainItemFactory.CreateInstance(newUser, DomainItemType.User, _propertiesToLoadResolver.Resolve(propsToLoad.GetPropertyLoader())).AsUser();
    }

    public async Task<ContainerItem> CreateContainerAsync(ContainerItem directory, string name, SearchQuery propsToLoad = null)
    {
        var container = await Task.Run(() => CreateContainer(directory, name, propsToLoad));
        return container;
    }
    
    public ContainerItem CreateContainer(ContainerItem directory, string name, SearchQuery propsToLoad = null)
    {
        var newContainer = _directoryEntryBuilder.SetType(DirectoryEntryType.organizationalUnit)
            .SetName(directory, name).Build();

        return _domainItemFactory.CreateInstance(newContainer, DomainItemType.Container, _propertiesToLoadResolver.Resolve(propsToLoad.GetPropertyLoader())).AsContainer();
    }

    public async Task<GroupItem> CreateGroupAsync(ContainerItem directory, string name, SearchQuery propsToLoad = null)
    {
        var group = await Task.Run(() => CreateGroup(directory, name, propsToLoad));
        return group;
    }

    public GroupItem CreateGroup(ContainerItem directory, string name, SearchQuery propsToLoad = null)
    {
        var newGroup = _directoryEntryBuilder.SetType(DirectoryEntryType.group)
            .SetName(directory, name).Build();

        return _domainItemFactory.CreateInstance(newGroup, DomainItemType.Group, _propertiesToLoadResolver.Resolve(propsToLoad.GetPropertyLoader())).AsGroup();
    }
}