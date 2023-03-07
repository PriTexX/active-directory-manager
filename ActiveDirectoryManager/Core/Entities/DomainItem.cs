using System.DirectoryServices;
using System.Runtime.Versioning;

namespace ActiveDirectoryManager.Core;

[SupportedOSPlatform("windows")]
public abstract class DomainItem
{
    internal DirectoryEntry? DirectoryEntry;
    internal SearchResult? SearchResult;
    private readonly List<string> _changedProperties = new ();
    private readonly List<string> _changedMultProperties = new ();
    private string? _distinguishedName;
    
    protected internal DomainItemType DomainItemType;
    protected internal readonly Dictionary<string, List<string?>> PropertyCollection = new (StringComparer.OrdinalIgnoreCase);

    public void Save()
    {
        RefreshDirectoryEntryCache();
        
        foreach (var propertyName in _changedProperties)
        {
            if (GetUnderlyingObject().Properties.Contains(propertyName))
            {
                GetUnderlyingObject().Properties[propertyName].Value = PropertyCollection[propertyName][0];
            }
            else
            {
                GetUnderlyingObject().Properties[propertyName].Add(PropertyCollection[propertyName][0]);
            }
        }

        foreach (var propertyName in _changedMultProperties)
        {
            if (GetUnderlyingObject().Properties.Contains(propertyName))
            {
                GetUnderlyingObject().Properties[propertyName].Clear();
            }
                
            foreach (var property in PropertyCollection[propertyName])
            {
                GetUnderlyingObject().Properties[propertyName].Add(property);
            }
                
        }
            
        GetUnderlyingObject().CommitChanges();
        _changedProperties.Clear();
        _changedMultProperties.Clear();
    }

    public async Task SaveAsync()
    {
        await Task.Run(Save);
    }

    public virtual void Remove()
    {
        GetUnderlyingObject().Parent.Children.Remove(GetUnderlyingObject());
    }

    public virtual async Task RemoveAsync()
    {
        await Task.Run(Remove);
    }

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

    public ContainerItem AsContainer()
    {
        return (ContainerItem)this;
    }
    
    public GroupItem AsGroup()
    {
        return (GroupItem)this;
    }

    public UserItem AsUser()
    {
        return (UserItem)this;
    }
    
    private void RefreshDirectoryEntryCache()
    {
        if (_changedProperties.Any() && _changedMultProperties.Any())
        {
            GetUnderlyingObject().RefreshCache(_changedProperties.Concat(_changedMultProperties).ToArray());
            return;
        }

        if (_changedProperties.Any())
        {
            GetUnderlyingObject().RefreshCache(_changedProperties.ToArray());
            return;
        }
        
        if (_changedMultProperties.Any())
        {
            GetUnderlyingObject().RefreshCache(_changedMultProperties.ToArray());
            return;
        }
    }
}