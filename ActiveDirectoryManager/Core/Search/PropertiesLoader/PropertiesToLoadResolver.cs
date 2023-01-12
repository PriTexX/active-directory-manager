namespace ActiveDirectoryManager.Core.Search.PropertiesLoader;

internal sealed class PropertiesToLoadResolver : IPropertyResolver
{
    private string[] _propertiesToLoad = { "adspath", "distinguishedname", "name" };

    public void SetStandardPropertiesToLoad(string[] propertiesToLoad)
    {
        _propertiesToLoad = propertiesToLoad;
    }

    public string[] Resolve(PropertyLoader? propertyLoader)
    {
        return propertyLoader is null ? _propertiesToLoad : (from pair in propertyLoader.PropertyCollection where pair.Value select pair.Key).ToArray<string>();
    }
}