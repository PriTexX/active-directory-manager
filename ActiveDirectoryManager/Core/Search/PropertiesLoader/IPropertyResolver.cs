namespace ActiveDirectoryManager.Core.Search.PropertiesLoader;

internal interface IPropertyResolver
{
    public void SetStandardPropertiesToLoad(string[] propertiesToLoad);
    public string[] Resolve(PropertyLoader? propertyLoader);
}