using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Core.Search.Common;

namespace ActiveDirectoryManager.Application.Factories;

public interface IDomainSearcherBuilder
{
    public IDomainSearcherBuilder SetStandardPropertiesToLoad(SearchQuery propertiesToLoad);
    public IDomainSearcherBuilder SetStandardPropertiesToLoad(string[] propertiesToLoad);
    public IDomainSearcherBuilder SetConnectionFactory(IActiveDirectoryConnectionFactory connectionFactory);
    public IDomainSearcherBuilder SetDomainItemFactory(IDomainItemFactory domainItemFactory);
    public IDomainSearcher Build();
}