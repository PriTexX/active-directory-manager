using System.Collections;
using System.DirectoryServices;
using System.Runtime.Versioning;
using ActiveDirectoryManager.Application.Factories;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Infrastructure.Factories;

[SupportedOSPlatform("windows")]
internal sealed class DomainItemFactory : IDomainItemFactory
{
    public DomainItem CreateInstance(SearchResult searchResult, DomainItemType type)
    {
        DomainItem domainItem = type switch
        {
            DomainItemType.User => new UserItem(),
            DomainItemType.Group => new GroupItem(),
            DomainItemType.Container => new ContainerItem(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        domainItem.SearchResult = searchResult;
        domainItem.DomainItemType = type;
        
        foreach (string ldapField in searchResult.Properties.PropertyNames)
        {
            var values = (from object? value in searchResult.Properties[ldapField] select value.ToString()).ToList();
            domainItem.PropertyCollection.Add(ldapField, values);
        }

        return domainItem;
    }
    
    public DomainItem CreateInstance(DirectoryEntry dirEntry, DomainItemType type, string[] propertiesToLoad)
    {
        DomainItem domainItem = type switch
        {
            DomainItemType.User => new UserItem(),
            DomainItemType.Group => new GroupItem(),
            DomainItemType.Container => new ContainerItem(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        domainItem.DirectoryEntry = dirEntry;
        domainItem.DomainItemType = type;
        
        domainItem.DirectoryEntry.RefreshCache(propertiesToLoad.Concat(new []{"distinguishedname"}).ToArray());
        
        foreach (var propertyName in propertiesToLoad)
        {
            PropertyValueCollection valueCollection = dirEntry.Properties[propertyName];
            domainItem.PropertyCollection.Add(propertyName, new List<string?>());
            IEnumerator en = valueCollection.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Current != null)
                {
                    domainItem.PropertyCollection[propertyName].Add(en.Current.ToString());
                }
            }
        }

        domainItem.DistinguishedName = domainItem.DirectoryEntry.Properties["distinguishedname"].Value.ToString();

        return domainItem;
    }
}