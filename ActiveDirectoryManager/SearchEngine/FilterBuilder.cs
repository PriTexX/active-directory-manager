using System.Text;
using ActiveDirectoryManager.ActiveDirectoryItem;

namespace ActiveDirectoryManager.SearchEngine;

internal static class FilterBuilder
{
    public static string BuildSearchFilter(QueryFilter queryFilter, DomainItemType type)
    {
        var filter = type switch
        {
            DomainItemType.Container => new StringBuilder("(&(objectClass=organizationalUnit)", 128),
            DomainItemType.Group => new StringBuilder("(&(objectCategory=group)", 128),
            DomainItemType.User => new StringBuilder("(&(sAMAccountType=805306368)", 128),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        filter.Append(queryFilter.ToStringFilter());
        
        filter.Append(')');
        
        return filter.ToString();
    }

    public static (string, IEnumerator<QueryFilter>, bool) BuildSearchFilter(IEnumerator<QueryFilter> enumerator, int maxCount, DomainItemType type)
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
            var queryFilter = enumerator.Current;

            filter.Append(queryFilter.ToStringFilter());
            
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

    public static (string, IEnumerator<string>, bool) BuildSearchFilter(IEnumerator<string> enumerator, int maxCount, 
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