using System.Text;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Core.Search.Filter;

internal class FilterBuilder
{
    public string BuildSearchFilter(string searchQuery, DomainItemType type)
    {
        var filter = type switch
        {
            DomainItemType.Container => new StringBuilder("(&(objectClass=organizationalUnit)", 128),
            DomainItemType.Group => new StringBuilder("(&(objectCategory=group)", 128),
            DomainItemType.User => new StringBuilder("(&(sAMAccountType=805306368)", 128),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        filter.Append(searchQuery);
        
        filter.Append(')');
        
        return filter.ToString();
    }
}