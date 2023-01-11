using System.DirectoryServices;
using System.Text.RegularExpressions;

namespace ActiveDirectoryManager.Core.Entities;

public abstract class DomainItem
{
    internal DirectoryEntry? DirectoryEntry;
    internal SearchResult? SearchResult;
    private readonly List<string> _changedProperties = new ();
    private readonly List<string> _changedMultProperties = new ();
    private string? _distinguishedName;
    
    protected internal DomainItemType DomainItemType;
    protected internal readonly Dictionary<string, List<string?>> PropertyCollection = new (StringComparer.OrdinalIgnoreCase);

    public string? Name => GetAttribute("name");

    public string? Description
    {
        get => GetAttribute("description");
        set => SetAttribute("description", value);
    }

    public string DistinguishedName
    {
        get
        {
            if (_distinguishedName is not null) return _distinguishedName;
            
            var adsPath = GetAttribute("adspath");
            var args = adsPath.Split('/');
            _distinguishedName = args[^1];
            
            return _distinguishedName;
        }
        internal set => _distinguishedName = value;
    }
    
    protected void SetAttribute(string key, string? val)
    {
        if (!PropertyCollection.ContainsKey(key))
        {
            PropertyCollection.Add(key, new List<string?>{val});
            
            if(!_changedProperties.Contains(key)) 
                _changedProperties.Add(key);
            return;
        }
        if (PropertyCollection[key].Count == 0)
        {
            PropertyCollection[key].Add(val);
        }
        else
        {
            PropertyCollection[key][0] = val;
        }

        if(!_changedProperties.Contains(key)) 
            _changedProperties.Add(key);
    }

    protected string? GetAttribute(string key)
    {
        if (!PropertyCollection.ContainsKey(key)) return null;
        return PropertyCollection[key].Count == 0 ? null : PropertyCollection[key][0];
    }

    protected void SetListAttributes(string key, List<string> val)
    {
        if (!PropertyCollection.ContainsKey(key))
        {
            PropertyCollection.Add(key, val);
            _changedMultProperties.Add(key);
            return;
        }
        PropertyCollection[key] = val;
        if(!_changedMultProperties.Contains(key))
            _changedMultProperties.Add(key);
    }
    
    protected List<string?>? GetListAttributes(string key)
    {
        return !PropertyCollection.ContainsKey(key) ? null : PropertyCollection[key];
    }
    
    public DirectoryEntry GetUnderlyingObject()
    {
        return DirectoryEntry ??= SearchResult.GetDirectoryEntry();
    }

    public GroupItem ToGroup()
    {
        return (GroupItem)this;
    }

    public UserItem ToUser()
    {
        return (UserItem)this;
    }
}