﻿using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Infrastructure.Common;

namespace ActiveDirectoryManager.Infrastructure.Factories;

public class ActiveDirectoryManagerFactory : IActiveDirectoryManagerFactory
{
    private IActiveDirectoryManager? _activeDirectoryManager;
    private IDomainSearcher? _domainSearcher;
    private IActiveDirectoryConnectionFactory? _connectionFactory;
    private IDomainSearcherBuilder _domainSearcherBuilder;

    public ActiveDirectoryManagerFactory(IDomainSearcherBuilder domainSearcherBuilder)
    {
        _domainSearcherBuilder = domainSearcherBuilder;
    }

    public static ActiveDirectoryManagerFactory Create()
    {
        return new ActiveDirectoryManagerFactory(new DomainSearcherBuilder());
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

        _activeDirectoryManager = new Infrastructure.Common.ActiveDirectoryManager(_connectionFactory);

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