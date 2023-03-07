﻿using ActiveDirectoryManager.Core;

namespace ActiveDirectoryManager.Application;

public interface IDomainSearcherBuilder
{
    public IDomainSearcherBuilder SetStandardPropertiesToLoad(SearchQuery propertiesToLoad);
    public IDomainSearcherBuilder SetStandardPropertiesToLoad(string[] propertiesToLoad);
    public IDomainSearcherBuilder SetConnectionFactory(IActiveDirectoryConnectionFactory connectionFactory);
    public IDomainSearcherBuilder SetDomainItemFactory(IDomainItemFactory domainItemFactory);
    public IDomainSearcher Build();
}