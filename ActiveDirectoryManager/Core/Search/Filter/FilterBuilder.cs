using System.Text;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Core.Search.Filter;

internal class FilterBuilder
{
    public string BuildSearchFilter(QueryFilter searchQuery, DomainItemType type)
    {
        var filter = type switch
        {
            DomainItemType.Container => new StringBuilder("(&(objectClass=organizationalUnit)", 128),
            DomainItemType.Group => new StringBuilder("(&(objectCategory=group)", 128),
            DomainItemType.User => new StringBuilder("(&(sAMAccountType=805306368)", 128),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        filter.Append(searchQuery.ToStringFilter());
        
        filter.Append(')');
        
        return filter.ToString();
    }

    public (string, IEnumerator<QueryFilter>, bool) BuildSearchFilter(IEnumerator<QueryFilter> enumerator, int maxCount, DomainItemType type)
    {
        var filter = new StringBuilder(2048);

        switch (type)
        {
            case DomainItemType.User: filter.Append("(&(sAMAccountType=805306368)(|");
                break;
            case DomainItemType.Group: filter.Append("(&(objectCategory=group)(|");
                break;
            case DomainItemType.Container: filter.Append("(&(objectCategory=organizationalUnit)(|"); 
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        var count = 0;
        
        var hasMore = enumerator.MoveNext();
        
        while (hasMore)
        {
            ++count;
            var searchQuery = enumerator.Current;

            filter.Append(searchQuery.ToStringFilter());
            
            hasMore = enumerator.MoveNext();

            if (count == maxCount || !hasMore)
            {
                filter.Append("))");
                return (filter.ToString(), enumerator, hasMore);
            }
        }
        
        filter.Append("))");
        return (filter.ToString(), enumerator, false);
    }

    public (string, IEnumerator<string>, bool) BuildSearchFilter(IEnumerator<string> enumerator, int maxCount, 
        DomainItemType type, string propertyName)
    {
        
        var filter = new StringBuilder(2048);

        switch (type)
        {
            case DomainItemType.User: filter.Append("(&(sAMAccountType=805306368)(|");
                break;
            case DomainItemType.Group: filter.Append("(&(objectCategory=group)(|");
                break;
            case DomainItemType.Container: filter.Append("(&(objectCategory=organizationalUnit)(|"); 
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        int count = 0;
        
        var hasMore = enumerator.MoveNext();

        while (hasMore)
        {
            ++count;
            var propertyValue = enumerator.Current;
            filter.Append($"({propertyName}={propertyValue})");
            hasMore = enumerator.MoveNext();

            if (count == maxCount || !hasMore)
            {
                filter.Append("))");
                return (filter.ToString(), enumerator, hasMore);
            }
        }
        
        filter.Append("))");
        return (filter.ToString(), enumerator, false);
    }
}