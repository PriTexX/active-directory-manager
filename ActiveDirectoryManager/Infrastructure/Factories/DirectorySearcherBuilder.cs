using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search;

namespace ActiveDirectoryManager.Infrastructure.Factories;

internal class DirectorySearcherBuilder : IDirectorySearcherBuilder
{
    private readonly FilterBuilder _filterBuilder;

    public DirectorySearcherBuilder(FilterBuilder filterBuilder)
    {
        _filterBuilder = filterBuilder;
    }

    public DirectorySearcher CreateInstance(PrincipalContext context, DomainItemType domainItemType, QueryFilter queryFilter, PropertyLoader propertyLoader)
    {
        var directorySearcher = (DirectorySearcher)GetPrincipalSearcher(context).GetUnderlyingSearcher();
        
        directorySearcher.Filter = _filterBuilder.BuildSearchFilter(queryFilter, domainItemType);
        directorySearcher.PropertiesToLoad.Clear();
        directorySearcher.PropertiesToLoad.AddRange(PropertiesToLoadResolver.Resolve(propertyLoader));
        
        return directorySearcher;
    }

    private PrincipalSearcher GetPrincipalSearcher(PrincipalContext context)
    {
        return new PrincipalSearcher(new GroupPrincipal(context));
    }
}