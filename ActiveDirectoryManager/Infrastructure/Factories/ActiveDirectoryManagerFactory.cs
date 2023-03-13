using System.Runtime.Versioning;
using ActiveDirectoryManager.Application;
using ActiveDirectoryManager.Core;

namespace ActiveDirectoryManager.Infrastructure;

[SupportedOSPlatform("windows")]
public sealed class ActiveDirectoryManagerFactory : IActiveDirectoryManagerFactory
{
    private IActiveDirectoryManager? _activeDirectoryManager;
    private IDomainSearcher? _domainSearcher;
    private IActiveDirectoryConnectionFactory? _connectionFactory;
    private IDomainSearcherBuilder _domainSearcherBuilder;
    private IPropertyResolver _propertiesToLoadResolver;
    private readonly IDomainItemFactory _domainItemFactory;

    internal ActiveDirectoryManagerFactory(IDomainSearcherBuilder domainSearcherBuilder, IPropertyResolver propertiesToLoadResolver, IDomainItemFactory domainItemFactory)
    {
        _domainSearcherBuilder = domainSearcherBuilder;
        _propertiesToLoadResolver = propertiesToLoadResolver;
        _domainItemFactory = domainItemFactory;
    }

    public static ActiveDirectoryManagerFactory Create()
    {
        return new ActiveDirectoryManagerFactory(new DomainSearcherBuilder(), new PropertiesToLoadResolver(), new DomainItemFactory());
    }

    public IActiveDirectoryManagerFactory SetConnectionOptions(string domain, string user, string password)
    {
        _connectionFactory = new ActiveDirectoryConnectionFactory(domain, user, password);
        return this;
    }

    public IActiveDirectoryManagerFactory ConfigureDomainSearcher(
        Func<IDomainSearcherBuilder, IDomainSearcherBuilder> domainSearcherConfigurator)
    {
        _domainSearcherBuilder = domainSearcherConfigurator(_domainSearcherBuilder);
        return this;
    }

    public IActiveDirectoryManagerFactory Build()
    {
        if (_connectionFactory is null)
            throw new ArgumentException("Connection options must be specified.");

        _activeDirectoryManager = new ActiveDirectoryManager(_connectionFactory.Domain, _domainItemFactory, _propertiesToLoadResolver);

        _domainSearcher = _domainSearcherBuilder
            .SetConnectionFactory(_connectionFactory)
            .SetDomainItemFactory(new DomainItemFactory())
            .Build();

        return this;
    }

    public IDomainSearcher GetDomainSearcher()
    {
        if (_domainSearcher is null)
            throw new Exception("Object must be initialized before using it");
        
        return _domainSearcher;
    }
    
    public IActiveDirectoryManager GetActiveDirectoryManager()
    {
        if (_activeDirectoryManager is null)
            throw new Exception("Object must be initialized before using it");
        
        return _activeDirectoryManager;
    }
}