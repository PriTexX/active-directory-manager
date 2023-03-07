using System.Text;

namespace ActiveDirectoryManager.Core;

internal sealed class QueryFilter
{
    private readonly Dictionary<string, List<string>?> _propertyCollection = new(StringComparer.OrdinalIgnoreCase);

    public List<string>? Name { get => GetProperty("name"); set => SetProperty("name", value); }
    public List<string>? SamAccountName { get => GetProperty("samaccountname"); set => SetProperty("samaccountname", value); }
    public List<string>? DistinguishedName { get => GetProperty("distinguishedname"); set => SetProperty("distinguishedname", value); }
    public List<string>? FirstName { get => GetProperty("givenname"); set => SetProperty("givenname", value); }
    public List<string>? Surname { get => GetProperty("sn"); set => SetProperty("sn", value); }
    public List<string>? UserPrincipalName { get => GetProperty("userprincipalname"); set => SetProperty("userprincipalname", value); }
    public List<string>? EmployeeNumber { get => GetProperty("employeenumber"); set => SetProperty("employeenumber", value); }
    public List<string>? DisplayName { get => GetProperty("displayname"); set => SetProperty("displayname", value); }

    public List<string>? GetProperty(string key)
    {
        _propertyCollection.TryGetValue(key, out var prop);
        return prop;
    }

    public void SetProperty(string key, List<string>? value)
    {
        var success = _propertyCollection.TryAdd(key, value);
        if (!success)
            _propertyCollection[key] = value;
    }

    public string ToStringFilter()
    {
        var filter = new StringBuilder("(&");
        
        foreach (var pair in _propertyCollection.Where(pair => pair.Value is not null))
        {
            if (pair.Value.Count > 1)
            {
                filter.Append("(|");
                foreach (var value in pair.Value)
                {
                    filter.Append($"({pair.Key}={value})");    
                }
                filter.Append(')');
            }
            else
            {
                filter.Append($"({pair.Key}={pair.Value[0]})");   
            }
        }
        
        filter.Append(')');
        
        return filter.ToString();
    }

    public static QueryFilter Empty()
    {
        return new QueryFilter();
    }
}