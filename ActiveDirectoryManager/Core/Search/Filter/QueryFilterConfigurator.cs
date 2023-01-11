namespace ActiveDirectoryManager.Core.Search.Filter;

public sealed class QueryFilterConfigurator
{
    private readonly QueryFilter _queryFilter;

    private QueryFilterConfigurator(QueryFilter queryFilter)
    {
        _queryFilter = queryFilter;
    }
    
    internal static QueryFilterConfigurator Create(QueryFilter queryFilter)
    {
        return new QueryFilterConfigurator(queryFilter);
    }

    public QueryFilterConfigurator AddCustomProperty(string propertyName, string value)
    {
        _queryFilter.SetProperty(propertyName, new List<string>{value});
        return this;
    }

    public QueryFilterConfigurator AddName(string value)
    {
        _queryFilter.Name = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddSamAccountName(string value)
    {
        _queryFilter.SamAccountName = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddDistinguishedName(string value)
    {
        _queryFilter.DistinguishedName = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddFirstName(string value)
    {
        _queryFilter.FirstName = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddSurname(string value)
    {
        _queryFilter.Surname = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddUserPrincipalName(string value)
    {
        _queryFilter.UserPrincipalName = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddEmployeeNumber(string value)
    {
        _queryFilter.EmployeeNumber = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddDisplayName(string value)
    {
        _queryFilter.DisplayName = new List<string>{value};
        return this;
    }
    
    public QueryFilterConfigurator AddCustomProperty(string propertyName, IEnumerable<string> value)
    {
        _queryFilter.SetProperty(propertyName, value.ToList());
        return this;
    }

    public QueryFilterConfigurator AddName(IEnumerable<string> value)
    {
        _queryFilter.Name = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddSamAccountName(IEnumerable<string> value)
    {
        _queryFilter.SamAccountName = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddDistinguishedName(IEnumerable<string> value)
    {
        _queryFilter.DistinguishedName = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddFirstName(IEnumerable<string> value)
    {
        _queryFilter.FirstName = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddSurname(IEnumerable<string> value)
    {
        _queryFilter.Surname = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddUserPrincipalName(IEnumerable<string> value)
    {
        _queryFilter.UserPrincipalName = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddEmployeeNumber(IEnumerable<string> value)
    {
        _queryFilter.EmployeeNumber = value.ToList();
        return this;
    }
    
    public QueryFilterConfigurator AddDisplayName(IEnumerable<string> value)
    {
        _queryFilter.DisplayName = value.ToList();
        return this;
    }
}