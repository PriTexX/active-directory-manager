using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryManager.ActiveDirectoryItem;
using ActiveDirectoryManager.ItemFactory;

namespace ActiveDirectoryManager.SearchEngine;

internal class DirectorySearcherBuilder
{
    private PrincipalContext _principalContext;
    private string _filter = "";
    private string[]? _propertiesToLoad;
    private DomainItemType _domainItemType = DomainItemType.User;

    public DirectorySearcher CreateInstance(PrincipalContext context, DomainItemType domainItemType, QueryFilter queryFilter, PropertyLoader propertyLoader)
    {
        _principalContext = context;
        _domainItemType = domainItemType;
        _filter = FilterBuilder.BuildSearchFilter(queryFilter, _domainItemType);
        _propertiesToLoad = PropertiesToLoadResolver.Resolve(propertyLoader);
            
            
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