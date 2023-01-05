namespace ActiveDirectoryManager.Core.Search.PropertiesLoader;

public class PropertyLoaderConfigurator
{
    private readonly PropertyLoader _propertyLoader;

    private PropertyLoaderConfigurator(PropertyLoader propertyLoader)
    {
        _propertyLoader = propertyLoader;
    }

    internal static PropertyLoaderConfigurator Create(PropertyLoader propertyLoader)
    {
        return new PropertyLoaderConfigurator(propertyLoader);
    }

    public PropertyLoaderConfigurator LoadCustom(string propertyName)
    {
        _propertyLoader.SetProperty(propertyName, true);
        return this;
    }

    public PropertyLoaderConfigurator LoadSamAccountName()
    {
        _propertyLoader.SamAccountName = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadDistinguishedName()
    {
        _propertyLoader.DistinguishedName = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadDescription()
    {
        _propertyLoader.Description = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadName()
    {
        _propertyLoader.Name = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadInfo()
    {
        _propertyLoader.Info = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadUserAccountControl()
    {
        _propertyLoader.UserAccountControl = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadFirstName()
    {
        _propertyLoader.FirstName = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadSurname()
    {
        _propertyLoader.Surname = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadDisplayName()
    {
        _propertyLoader.DisplayName = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadUserPrincipalName()
    {
        _propertyLoader.UserPrincipalName = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadEmployeeNumber()
    {
        _propertyLoader.EmployeeNumber = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadMail()
    {
        _propertyLoader.Mail = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadDepartment()
    {
        _propertyLoader.Department = true;
        return this;
    }
    
    public PropertyLoaderConfigurator LoadCompany()
    {
        _propertyLoader.Company = true;
        return this;
    }

    public PropertyLoaderConfigurator LoadProperties(IEnumerable<string> propertyNames)
    {
        foreach (var property in propertyNames)
        {
            _propertyLoader.SetProperty(property, true);
        }

        return this;
    }
}