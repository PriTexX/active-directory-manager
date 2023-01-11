using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search.Filter;

namespace ActiveDirectoryManager.Core.Search.Engine;

internal class DirectorySearcherBuilder
{
    private readonly FilterBuilder _filterBuilder;

    public DirectorySearcherBuilder(FilterBuilder filterBuilder)
    {
        _filterBuilder = filterBuilder;
    }

    public DirectorySearcher CreateInstance(PrincipalContext context, DomainItemType domainItemType, string queryFilter, string[] propertiesToLoad)
    {
        var directorySearcher = (DirectorySearcher)GetPrincipalSearcher(context).GetUnderlyingSearcher();
        
        directorySearcher.Filter = _filterBuilder.BuildSearchFilter(queryFilter, domainItemType);
        directorySearcher.PropertiesToLoad.Clear();
        directorySearcher.PropertiesToLoad.AddRange(propertiesToLoad);
        
        return directorySearcher;
    }

    private PrincipalSearcher GetPrincipalSearcher(PrincipalContext context)
    {
        return new PrincipalSearcher(new GroupPrincipal(context));
    }
}