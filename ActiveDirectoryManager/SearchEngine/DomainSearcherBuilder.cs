using ActiveDirectoryManager.ConnectionFactory;
using ActiveDirectoryManager.ItemFactory;

namespace ActiveDirectoryManager.SearchEngine;

public class DomainSearcherBuilder
{
    private IActiveDirectoryConnectionFactory? _connectionFactory;
    private IDomainItemFactory? _domainItemFactory;

    public DomainSearcherBuilder SetStandardPropertiesToLoad(PropertyLoader propertiesToLoad)
    {
        PropertiesToLoadResolver.SetStandardPropertiesToLoad(propertiesToLoad);
        return this;
    }

    public DomainSearcherBuilder SetConnectionFactory(IActiveDirectoryConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        return this;
    }

    public DomainSearcherBuilder SetDomainItemFactory(IDomainItemFactory domainItemFactory)
    {
        _domainItemFactory = domainItemFactory;
        return this;
    }

    public DomainSearcher Build()
    {
        if (_connectionFactory is null)
            throw new ArgumentException("ConnectionFactory must be specified in DomainSearchBuilder before building a DomainSearcher");

        return new DomainSearcher(_domainItemFactory ?? new DomainItemFactory(), _connectionFactory);
    }
}