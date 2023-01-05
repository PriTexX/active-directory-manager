using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search;

namespace ActiveDirectoryManager.Application.Factories;

public interface IDirectorySearcherBuilder
{
    public DirectorySearcher CreateInstance(PrincipalContext context, DomainItemType domainItemType,
        QueryFilter queryFilter, PropertyLoader propertyLoader);
}