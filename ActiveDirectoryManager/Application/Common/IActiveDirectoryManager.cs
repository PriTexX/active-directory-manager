using ActiveDirectoryManager.Core;

namespace ActiveDirectoryManager.Application;

public interface IActiveDirectoryManager
{
    public string Domain { get; }
    public Task AddToGroupAsync(DomainItem item, GroupItem groupItem);
    
    public void AddToGroup(DomainItem item, GroupItem groupItem);
    
    public Task RemoveFromGroupAsync(DomainItem item, GroupItem groupItem);
    
    public void RemoveFromGroup(DomainItem item, GroupItem groupItem);
    
    public Task RenameAsync(DomainItem item, string newName);
    
    public void Rename(DomainItem item, string newName);

    public Task MoveToAsync(DomainItem item, ContainerItem containerItem);
    
    public void MoveTo(DomainItem item, ContainerItem containerItem);
    
    public Task CopyToAsync(DomainItem item, ContainerItem containerItem);
    public Task CopyToAsync(DomainItem item, ContainerItem containerItem, string newName);
    
    public void CopyTo(DomainItem item, ContainerItem containerItem);
    public void CopyTo(DomainItem item, ContainerItem containerItem, string newName);

    public UserItem CreateUser(ContainerItem directory, string name, string userPassword, SearchQuery? propsToLoad = null);
    public ContainerItem CreateContainer(ContainerItem directory, string name, SearchQuery? propsToLoad = null);
    public GroupItem CreateGroup(ContainerItem directory, string name, SearchQuery? propsToLoad = null);
    
    public Task<UserItem> CreateUserAsync(ContainerItem directory, string name, string userPassword, SearchQuery? propsToLoad = null);
    public Task<ContainerItem> CreateContainerAsync(ContainerItem directory, string name, SearchQuery? propsToLoad = null);
    public Task<GroupItem> CreateGroupAsync(ContainerItem directory, string name, SearchQuery? propsToLoad = null);
}