using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryManager.ActiveDirectoryItem;

namespace ActiveDirectoryManager.SearchEngine;

internal class DirectorySearcherFactory
{
    private PrincipalContext _principalContext;
    private string _filter = "";
    private string[]? _propertiesToLoad;
    private DomainItemType _domainItemType = DomainItemType.User;
    
    public static DirectorySearcherFactory CreateFactory(PrincipalContext context)
    {
        var factory = new DirectorySearcherFactory
        {
            _principalContext = context
        };
        return factory;
    }

    public DirectorySearcherFactory SetDomainItemType(DomainItemType domainItemType)
    {
        _domainItemType = domainItemType;
        return this;
    }

    public DirectorySearcherFactory SetFilter(QueryFilter queryFilter)
    {
        _filter = FilterBuilder.BuildSearchFilter(queryFilter, _domainItemType);
        return this;
    }

    public DirectorySearcherFactory SetPropertiesToLoad(PropertyLoader propertyLoader)
    {
        _propertiesToLoad = PropertiesToLoadResolver.Resolve(propertyLoader);
        return this;
    }

    public DirectorySearcher Build()
    {
        _propertiesToLoad ??= PropertiesToLoadResolver.GetStandardPropertiesToLoad();
            
        var directorySearcher = (DirectorySearcher)GetPrincipalSearcher().GetUnderlyingSearcher();
        
        directorySearcher.Filter = _filter;
        
        directorySearcher.PropertiesToLoad.Clear();
        directorySearcher.PropertiesToLoad.AddRange(_propertiesToLoad);

        return directorySearcher;
    }
    
    private PrincipalSearcher GetPrincipalSearcher()
    {
        return new PrincipalSearcher(new GroupPrincipal(_principalContext));
    }
}