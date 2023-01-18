using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Application.Common;

public interface IActiveDirectoryManager
{
    public Task AddToGroupAsync(DomainItem item, GroupItem groupItem);
    
    public void AddToGroup(DomainItem item, GroupItem groupItem);
    
    public Task RemoveFromGroupAsync(DomainItem item, GroupItem groupItem);
    
    public void RemoveFromGroup(DomainItem item, GroupItem groupItem);
    
    public Task RenameAsync(DomainItem item, string newName);
    
    public void Rename(DomainItem item, string newName);

    public Task MoveToAsync(DomainItem item, ContainerItem containerItem);
    
    public void MoveTo(DomainItem item, ContainerItem containerItem);
    
    public Task CopyToAsync(DomainItem item, ContainerItem containerItem);
    
    public void CopyTo(DomainItem item, ContainerItem containerItem);

    public UserItem CreateUser(ContainerItem directory, string name, string userPassword);
    public ContainerItem CreateContainer(ContainerItem directory, string name);
    public GroupItem CreateGroup(ContainerItem directory, string name);
    
    public Task<UserItem> CreateUserAsync(ContainerItem directory, string name, string userPassword);
    public Task<ContainerItem> CreateContainerAsync(ContainerItem directory, string name);
    public Task<GroupItem> CreateGroupAsync(ContainerItem directory, string name);
}