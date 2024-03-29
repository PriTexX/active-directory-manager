﻿using System.Runtime.Versioning;
using ActiveDirectoryManager.Application;
using ActiveDirectoryManager.Core;

namespace ActiveDirectoryManager.Infrastructure;

[SupportedOSPlatform("windows")]
public sealed class ActiveDirectoryManager : IActiveDirectoryManager // TODO: Сделать возможность делать ретрай операций
{
    private readonly IPropertyResolver _propertiesToLoadResolver;
    private readonly IDomainItemFactory _domainItemFactory;

    internal ActiveDirectoryManager(string domain, IDomainItemFactory domainItemFactory, IPropertyResolver propertiesToLoadResolver)
    {
        _domainItemFactory = domainItemFactory;
        _propertiesToLoadResolver = propertiesToLoadResolver;
        Domain = domain;
    }

    public string Domain { get; }

    public async Task AddToGroupAsync(DomainItem item, GroupItem groupItem)
    {
        await Task.Run(() => AddToGroup(item, groupItem));
    }
    
    public void AddToGroup(DomainItem item, GroupItem groupItem)
    {
        groupItem.GetUnderlyingObject().Properties["member"].Add(item.DistinguishedName);
        groupItem.Save();
    }

    public async Task RemoveFromGroupAsync(DomainItem item, GroupItem groupItem)
    {
        await Task.Run(() => RemoveFromGroup(item, groupItem));
    }
    
    public void RemoveFromGroup(DomainItem item, GroupItem groupItem)
    {
        groupItem.GetUnderlyingObject().Properties["member"].Remove(item.DistinguishedName);
        groupItem.Save();
    }

    public async Task RenameAsync(DomainItem item, string newName)
    {
        await Task.Run(() => Rename(item, newName));
    }
    
    public void Rename(DomainItem item, string newName)
    {
        item.GetUnderlyingObject().Rename("CN=" + newName);
        item.GetUnderlyingObject().RefreshCache(new []{"distinguishedname"});
        
        item.DistinguishedName = item.GetUnderlyingObject().Properties["distinguishedname"].Value!.ToString()!;
            
        if (item.PropertyCollection.ContainsKey("name"))
        {
            item.PropertyCollection["name"][0] = newName;
        }
    }

    public async Task MoveToAsync(DomainItem item, ContainerItem containerItem)
    {
        await Task.Run(() => MoveTo(item, containerItem));
    }
    
    public void MoveTo(DomainItem item, ContainerItem containerItem)
    {
        item.GetUnderlyingObject().MoveTo(containerItem.GetUnderlyingObject(), item.GetUnderlyingObject().Name);
        item.GetUnderlyingObject().RefreshCache(new []{"distinguishedname"});
        item.DistinguishedName = item.GetUnderlyingObject().Properties["distinguishedname"].Value!.ToString()!;
    }

    public async Task CopyToAsync(DomainItem item, ContainerItem containerItem)
    {
        await Task.Run(() => CopyTo(item, containerItem));
    }

    public async Task CopyToAsync(DomainItem item, ContainerItem containerItem, string newName)
    {
        await Task.Run(() => CopyTo(item, containerItem, newName));
    }

    public void CopyTo(DomainItem item, ContainerItem containerItem)
    {
        item.GetUnderlyingObject().CopyTo(containerItem.GetUnderlyingObject());
    }

    public void CopyTo(DomainItem item, ContainerItem containerItem, string newName)
    {
        item.GetUnderlyingObject().CopyTo(containerItem.GetUnderlyingObject(), newName);
    }

    public async Task<UserItem> CreateUserAsync(ContainerItem directory, string name, string userPassword, string samAccountName, SearchQuery? propsToLoad = null)
    {
        var user = await Task.Run(() => CreateUser(directory, name, userPassword, samAccountName, propsToLoad));
        return user;
    }
    
    public UserItem CreateUser(ContainerItem directory, string name, string userPassword, string samAccountName, SearchQuery? propsToLoad = null)
    {
        var newUser = directory.GetUnderlyingObject().Children.Add($"CN={name}", "user");
        
        newUser.Properties["samAccountName"].Value = samAccountName;
        newUser.CommitChanges();

        newUser.Properties["userAccountControl"].Value = 512;
        newUser.Invoke("SetPassword", userPassword);
        newUser.CommitChanges();
        
        return _domainItemFactory.CreateInstance(newUser, DomainItemType.User, _propertiesToLoadResolver.Resolve(propsToLoad?.GetPropertyLoader())).AsUser();
    }

    public async Task<ContainerItem> CreateContainerAsync(ContainerItem directory, string name, SearchQuery? propsToLoad = null)
    {
        var container = await Task.Run(() => CreateContainer(directory, name, propsToLoad));
        return container;
    }
    
    public ContainerItem CreateContainer(ContainerItem directory, string name, SearchQuery? propsToLoad = null)
    {
        var newContainer = directory.GetUnderlyingObject()
            .Children.Add($"OU={name}", "organizationalUnit");
        newContainer.CommitChanges();
        

        return _domainItemFactory.CreateInstance(newContainer, DomainItemType.Container, _propertiesToLoadResolver.Resolve(propsToLoad?.GetPropertyLoader())).AsContainer();
    }

    public async Task<GroupItem> CreateGroupAsync(ContainerItem directory, string name, SearchQuery? propsToLoad = null)
    {
        var group = await Task.Run(() => CreateGroup(directory, name, propsToLoad));
        return group;
    }

    public GroupItem CreateGroup(ContainerItem directory, string name, SearchQuery? propsToLoad = null)
    {
        var newGroup = directory.GetUnderlyingObject()
            .Children.Add($"CN={name}", "group");
        newGroup.CommitChanges();
       
        return _domainItemFactory.CreateInstance(newGroup, DomainItemType.Group, _propertiesToLoadResolver.Resolve(propsToLoad?.GetPropertyLoader())).AsGroup();
    }
    
    public void Enable(UserItem userItem)
    {
        var val = (int)userItem.GetUnderlyingObject().Properties["userAccountControl"].Value;
        userItem.GetUnderlyingObject().Properties["userAccountControl"].Value = val & ~0x2;
    }

    public void Disable(UserItem userItem)
    {
        var val = (int)userItem.GetUnderlyingObject().Properties["userAccountControl"].Value;
        userItem.GetUnderlyingObject().Properties["userAccountControl"].Value = val | 0x2;
    }

    public async Task EnableAsync(UserItem userItem)
    {
        await Task.Run(() => EnableAsync(userItem));
    }

    public async Task DisableAsync(UserItem userItem)
    {
        await Task.Run(() => DisableAsync(userItem));
    }
}