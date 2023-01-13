using System.Runtime.Versioning;
using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Search.Common;
using ActiveDirectoryManager.Core.Search.Engine;
using ActiveDirectoryManager.Core.Search.Filter;
using ActiveDirectoryManager.Core.Search.PropertiesLoader;
using ActiveDirectoryManager.Infrastructure.Common;

namespace ActiveDirectoryManager.Infrastructure.Factories;

[SupportedOSPlatform("windows")]
public sealed class DomainSearcherBuilder : IDomainSearcherBuilder
{
    private IActiveDirectoryConnectionFactory? _connectionFactory;
    private IDomainItemFactory? _domainItemFactory;
    private readonly DirectorySearcherBuilder _directorySearcherBuilder = new (new FilterBuilder()); 
    private readonly IPropertyResolver _propertiesToLoadResolver = new PropertiesToLoadResolver();

    public IDomainSearcherBuilder SetStandardPropertiesToLoad(SearchQuery propertiesToLoad)
    {
        _propertiesToLoadResolver.SetStandardPropertiesToLoad(_propertiesToLoadResolver.Resolve(propertiesToLoad.GetPropertyLoader()));
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

        return new DomainSearcher(_domainItemFactory ?? throw new Exception("Factory must be initialized"), _connectionFactory, _propertiesToLoadResolver, _directorySearcherBuilder);
    }
}