using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Infrastructure.Common;

public sealed class ActiveDirectoryManager : IActiveDirectoryManager
{
    private IActiveDirectoryConnectionFactory _connectionFactory;

    public ActiveDirectoryManager(IActiveDirectoryConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void AddToGroup(DomainItem item, GroupItem groupItem)
    {
        throw new NotImplementedException();
    }

    public Task AddToGroupAsync(DomainItem item, GroupItem groupItem)
    {
        throw new NotImplementedException();
    }

    public void RemoveFromGroup(DomainItem item, GroupItem groupItem)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromGroupAsync(DomainItem item, GroupItem groupItem)
    {
        throw new NotImplementedException();
    }

    public void Rename(DomainItem item)
    {
        throw new NotImplementedException();
    }

    public Task RenameAsync(DomainItem item)
    {
        throw new NotImplementedException();
    }

    public void MoveTo(DomainItem item, ContainerItem containerItem)
    {
        throw new NotImplementedException();
    }

    public Task MoveToAsync(DomainItem item, ContainerItem containerItem)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(DomainItem item, ContainerItem containerItem)
    {
        throw new NotImplementedException();
    }

    public Task CopyToAsync(DomainItem item, ContainerItem containerItem)
    {
        throw new NotImplementedException();
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