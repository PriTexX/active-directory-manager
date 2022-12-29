using System.Text;

namespace ActiveDirectoryManager.SearchEngine;

public class QueryFilter
{
    protected readonly Dictionary<string, string?> PropertyCollection = new(StringComparer.OrdinalIgnoreCase);

    public string? Name { get => GetProperty("name"); set => SetProperty("name", value); }
    public string? SamAccountName { get => GetProperty("samaccountname"); set => SetProperty("samaccountname", value); }
    public string? DistinguishedName { get => GetProperty("distinguishedname"); set => SetProperty("distinguishedname", value); }
    public string? FirstName { get => GetProperty("givenname"); set => SetProperty("givenname", value); }
    public string? Surname { get => GetProperty("sn"); set => SetProperty("sn", value); }
    public string? UserPrincipalName { get => GetProperty("userprincipalname"); set => SetProperty("userprincipalname", value); }
    public string? EmployeeNumber { get => GetProperty("employeenumber"); set => SetProperty("employeenumber", value); }
    public string? DisplayName { get => GetProperty("displayname"); set => SetProperty("displayname", value); }

    protected string? GetProperty(string key)
    {
        PropertyCollection.TryGetValue(key, out var prop);
        return prop;
    }

    protected void SetProperty(string key, string? value)
    {
        var success = PropertyCollection.TryAdd(key, value);
        if (!success)
            PropertyCollection[key] = value;
    }

    public virtual string ToStringFilter()
    {
        var filter = new StringBuilder("(&");
        
        foreach (var pair in PropertyCollection.Where(pair => pair.Value is not null))
        {
            filter.Append($"({pair.Key}={pair.Value})");
        }
        
        filter.Append(')');
        
        return filter.ToString();
    }
}