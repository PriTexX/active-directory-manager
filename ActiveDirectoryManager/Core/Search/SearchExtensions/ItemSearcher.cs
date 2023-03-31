using ActiveDirectoryManager.Application;

namespace ActiveDirectoryManager.Core;

public struct ItemSearcher
{
    private readonly IDomainSearcher _domainSearcher;

    public ItemSearcher()
    {
        throw new NotSupportedException();
    }
    
    internal ItemSearcher(IDomainSearcher domainSearcher)
    {
        _domainSearcher = domainSearcher;
    }


    public UserSearch User => new(_domainSearcher);
    public GroupSearch Group => new(_domainSearcher);
    public ContainerSearch Container => new(_domainSearcher);
}