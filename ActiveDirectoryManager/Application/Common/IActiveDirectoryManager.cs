using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Application.Common;

public interface IActiveDirectoryManager
{
    public void AddToGroup(DomainItem item, GroupItem groupItem);
    public Task AddToGroupAsync(DomainItem item, GroupItem groupItem);

    public void RemoveFromGroup(DomainItem item, GroupItem groupItem);
    public Task RemoveFromGroupAsync(DomainItem item, GroupItem groupItem);

    public void Rename(DomainItem item);
    public Task RenameAsync(DomainItem item);

    public void MoveTo(DomainItem item, ContainerItem containerItem);
    public Task MoveToAsync(DomainItem item, ContainerItem containerItem);
    
    public void CopyTo(DomainItem item, ContainerItem containerItem);
    public Task CopyToAsync(DomainItem item, ContainerItem containerItem);

    public UserItem CreateUser(ContainerItem directory, string name, string userPassword);
    public ContainerItem CreateContainer(ContainerItem directory, string name);
    public GroupItem CreateGroup(ContainerItem directory, string name);
    
    public Task<UserItem> CreateUserAsync(ContainerItem directory, string name, string userPassword);
    public Task<ContainerItem> CreateContainerAsync(ContainerItem directory, string name);
    public Task<GroupItem> CreateGroupAsync(ContainerItem directory, string name);
}