using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Core.Search;

namespace ActiveDirectoryManager.Application.Factories;

public interface IDomainSearcherBuilder
{
    public IDomainSearcherBuilder SetStandardPropertiesToLoad(PropertyLoader propertiesToLoad);
    public IDomainSearcherBuilder SetConnectionFactory(IActiveDirectoryConnectionFactory connectionFactory);
    public IDomainSearcherBuilder SetDomainItemFactory(IDomainItemFactory domainItemFactory);
    public IDomainSearcher Build();
}