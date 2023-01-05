namespace ActiveDirectoryManager.Core.Search;

public class PropertyLoader
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

    public bool DistinguishedName { get; set; } = false;
    public bool Description { get; set; } = false;
    public bool Name { get; set; } = false;
    public bool Info { get; set; } = false;
    public bool UserAccountControl { get; set; } = false;
    public bool FirstName { get; set; } = false;
    public bool Surname { get; set; } = false;
    public bool DisplayName { get; set; } = false;
    public bool UserPrincipalName { get; set; } = false;
    public bool SamAccountName { get; set; } = false;
    public bool EmployeeNumber { get; set; } = false;
    public bool Mail { get; set; } = false;
    public bool Department { get; set; } = false;
    public bool Company { get; set; } = false;

    public static PropertyLoader GetDefault()
    {
        return new PropertyLoader
        {
            DistinguishedName = true,
            Name = true
        };
    }
}