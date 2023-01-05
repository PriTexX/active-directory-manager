namespace ActiveDirectoryManager.Core.Search.Filter;

public class QueryFilterConfigurator
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
        _queryFilter.SetProperty(propertyName, value);
        return this;
    }

    public QueryFilterConfigurator AddName(string value)
    {
        _queryFilter.Name = value;
        return this;
    }
    
    public QueryFilterConfigurator AddSamAccountName(string value)
    {
        _queryFilter.SamAccountName = value;
        return this;
    }
    
    public QueryFilterConfigurator AddDistinguishedName(string value)
    {
        _queryFilter.DistinguishedName = value;
        return this;
    }
    
    public QueryFilterConfigurator AddFirstName(string value)
    {
        _queryFilter.FirstName = value;
        return this;
    }
    
    public QueryFilterConfigurator AddSurname(string value)
    {
        _queryFilter.Surname = value;
        return this;
    }
    
    public QueryFilterConfigurator AddUserPrincipalName(string value)
    {
        _queryFilter.UserPrincipalName = value;
        return this;
    }
    
    public QueryFilterConfigurator AddEmployeeNumber(string value)
    {
        _queryFilter.EmployeeNumber = value;
        return this;
    }
    
    public QueryFilterConfigurator AddDisplayName(string value)
    {
        _queryFilter.DisplayName = value;
        return this;
    }
}