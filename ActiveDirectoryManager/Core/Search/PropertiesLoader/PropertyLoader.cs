namespace ActiveDirectoryManager.Core;

public sealed class PropertyLoader
{
    internal readonly Dictionary<string, bool> PropertyCollection = new(StringComparer.OrdinalIgnoreCase);
    public bool GetProperty(string key)
    {
        PropertyCollection.TryGetValue(key, out var prop);
        return prop;
    }

    public void SetProperty(string key, bool value)
    {
        var success = PropertyCollection.TryAdd(key, value);
        if (!success)
            PropertyCollection[key] = value;
    }

    public bool IsEmpty()
    {
        return PropertyCollection.Count == 0;
    }

    public bool SamAccountName { get => GetProperty("samaccountname"); set => SetProperty("samaccountname", value); }
    public bool DistinguishedName { get => GetProperty("distinguishedName"); set => SetProperty("distinguishedName", value); }
    public bool Description { get => GetProperty("description"); set => SetProperty("description", value); }
    public bool Name { get => GetProperty("name"); set => SetProperty("name", value); }
    public bool Info { get => GetProperty("info"); set => SetProperty("info", value); }
    public bool UserAccountControl { get => GetProperty("userAccountControl"); set => SetProperty("userAccountControl", value); }
    public bool FirstName { get => GetProperty("givenname"); set => SetProperty("givenname", value); }
    public bool Surname { get => GetProperty("sn"); set => SetProperty("sn", value); }
    public bool DisplayName { get => GetProperty("displayName"); set => SetProperty("displayName", value); }
    public bool UserPrincipalName { get => GetProperty("userPrincipalName"); set => SetProperty("userPrincipalName", value); }
    public bool EmployeeNumber { get => GetProperty("employeeNumber"); set => SetProperty("employeeNumber", value); }
    public bool Mail { get => GetProperty("mail"); set => SetProperty("mail", value); }
    public bool Department { get => GetProperty("department"); set => SetProperty("department", value); }
    public bool Company { get => GetProperty("company"); set => SetProperty("company", value); }

    public static PropertyLoader GetDefault()
    {
        return new PropertyLoader
        {
            DistinguishedName = true,
            Name = true
        };
    }

    public static PropertyLoader Empty()
    {
        return new PropertyLoader();
    }
}