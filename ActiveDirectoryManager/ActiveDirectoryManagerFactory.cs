using ActiveDirectoryManager.ConnectionFactory;
using ActiveDirectoryManager.SearchEngine;

namespace ActiveDirectoryManager;

public class ActiveDirectoryManagerFactory
{
    private ActiveDirectoryManager? _activeDirectoryManager;
    private DomainSearcher? _domainSearcher;
    private IActiveDirectoryConnectionFactory? _connectionFactory;
    private DomainSearcherBuilder _domainSearcherBuilder = new ();
    
    public static ActiveDirectoryManagerFactory Create()
    {
        return new ActiveDirectoryManagerFactory();
    }

    public ActiveDirectoryManagerFactory SetConnectionOptions(string domain, string user, string password)
    {
        _connectionFactory = new ActiveDirectoryConnectionFactory(domain, user, password);
        return this;
    }

    public ActiveDirectoryManagerFactory ConfigureDomainSearcher(
        Func<DomainSearcherBuilder, DomainSearcherBuilder> domainSearcherConfigurator)
    {
        _domainSearcherBuilder = domainSearcherConfigurator(_domainSearcherBuilder);
        return this;
    }

    public ActiveDirectoryManagerFactory Build()
    {
        if (_connectionFactory is null)
            throw new ArgumentException("Connection options must be specified.");

        _activeDirectoryManager = new ActiveDirectoryManager(_connectionFactory);

        _domainSearcher = _domainSearcherBuilder
            .SetConnectionFactory(_connectionFactory)
            .Build();

        return this;
    }

    public DomainSearcher GetDomainSearcher()
    {
        if (_domainSearcher is null)
            throw new Exception("Object must be initialized before using it");
        
        return _domainSearcher;
    }
    
    public ActiveDirectoryManager GetActiveDirectoryManager()
    {
        if (_activeDirectoryManager is null)
            throw new Exception("Object must be initialized before using it");
        
        return _activeDirectoryManager;
    }
}