﻿using ActiveDirectoryManager.Application.Common;

namespace ActiveDirectoryManager.Application.Factories;

public interface IActiveDirectoryManagerFactory
{
    public IActiveDirectoryManagerFactory SetConnectionOptions(string domain, string user, string password);

    public IActiveDirectoryManagerFactory ConfigureDomainSearcher(
        Func<IDomainSearcherBuilder, IDomainSearcherBuilder> domainSearcherConfigurator);

    public IActiveDirectoryManagerFactory Build();
    public IDomainSearcher GetDomainSearcher();
    public IActiveDirectoryManager GetActiveDirectoryManager();
}