namespace ActiveDirectoryManager.SearchEngine;

internal static class PropertiesToLoadResolver
{
    private static string[] _propertiesToLoad = { "adspath", "distinguishedname", "name" };
    
    public static void SetStandardPropertiesToLoad(PropertyLoader propertyLoader)
    {
        _propertiesToLoad = Resolve(propertyLoader);
    }

    public static string[] Resolve(PropertyLoader? propertyLoader)
    {
        return propertyLoader is null ? _propertiesToLoad : (from pair in propertyLoader.PropertyCollection where pair.Value select pair.Key).ToArray();
    }

    public static string[] GetStandardPropertiesToLoad()
    {
        return _propertiesToLoad;
    }
}