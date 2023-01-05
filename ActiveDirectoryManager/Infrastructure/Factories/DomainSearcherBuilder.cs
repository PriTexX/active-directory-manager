using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;
using ActiveDirectoryManager.Infrastructure.Common;

namespace ActiveDirectoryManager.Infrastructure.Factories;

public class DomainSearcherBuilder : IDomainSearcherBuilder
{
    private IActiveDirectoryConnectionFactory? _connectionFactory;
    private IDomainItemFactory? _domainItemFactory;
    private readonly PropertiesToLoadResolver _propertiesToLoadResolver = new ();

    public IDomainSearcherBuilder SetStandardPropertiesToLoad(SearchQuery propertiesToLoad)
    {
        _propertiesToLoadResolver.SetStandardPropertiesToLoad(propertiesToLoad.GetPropertyLoader());
        return this;
    }

    public IDomainSearcherBuilder SetStandardPropertiesToLoad(string[] propertiesToLoad)
    {
        _propertiesToLoadResolver.SetStandardPropertiesToLoad(propertiesToLoad);
        return this;
    }

    public IDomainSearcherBuilder SetConnectionFactory(IActiveDirectoryConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        return this;
    }

    public IDomainSearcherBuilder SetDomainItemFactory(IDomainItemFactory domainItemFactory)
    {
        _domainItemFactory = domainItemFactory;
        return this;
    }

    public IDomainSearcher Build()
    {
        if (_connectionFactory is null)
            throw new ArgumentException("ConnectionFactory must be specified in DomainSearchBuilder before building a DomainSearcher");

        return new DomainSearcher(_domainItemFactory ?? new DomainItemFactory(), _connectionFactory, _propertiesToLoadResolver);
    }
}