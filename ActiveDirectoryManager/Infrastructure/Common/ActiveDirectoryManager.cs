using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Infrastructure.Common;

public sealed class ActiveDirectoryManager : IActiveDirectoryManager // TODO: Сделать возможность делать ретрай операций
{
    private IActiveDirectoryConnectionFactory _connectionFactory;

    public ActiveDirectoryManager(IActiveDirectoryConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
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

    public UserItem CreateUser(ContainerItem directory, string name, string userPassword)
    {
        throw new NotImplementedException();
    }

    public ContainerItem CreateContainer(ContainerItem directory, string name)
    {
        throw new NotImplementedException();
    }

    public GroupItem CreateGroup(ContainerItem directory, string name)
    {
        throw new NotImplementedException();
    }

    public Task<UserItem> CreateUserAsync(ContainerItem directory, string name, string userPassword)
    {
        throw new NotImplementedException();
    }

    public Task<ContainerItem> CreateContainerAsync(ContainerItem directory, string name)
    {
        throw new NotImplementedException();
    }

    public Task<GroupItem> CreateGroupAsync(ContainerItem directory, string name)
    {
        throw new NotImplementedException();
    }
}