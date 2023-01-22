using System.DirectoryServices;
using ActiveDirectoryManager.Core.Entities;

namespace ActiveDirectoryManager.Core.DirEntry;

internal sealed class DirectoryEntryBuilder
{
    private DirectoryEntry _directoryEntry;
    private DirectoryEntryType _directoryEntryType;

    public DirectoryEntryBuilder SetType(DirectoryEntryType type)
    {
        _directoryEntryType = type;

        return this;
    }

    public DirectoryEntryBuilder SetName(DomainItem directory, string name)
    {
        try
        {
            _directoryEntry = directory.GetUnderlyingObject()
                .Children.Add($"OU={name}", $"{_directoryEntryType.ToString()}");
            _directoryEntry.CommitChanges();
        }
        catch (Exception e)
        {
            throw e;
        }
        
        return this;
    }

    public DirectoryEntryBuilder SetProperty(string propertyName, string value)
    {
        try
        {
            _directoryEntry.Properties[propertyName].Value = value;
        }
        catch (Exception e)
        {
            throw e;
        }

        return this;
    }

    public DirectoryEntryBuilder SetPassword(string password)
    {
        try
        {
            _directoryEntry.Invoke("SetPassword", password);

        }
        catch (Exception e)
        {
            throw e;
        }
        
        try
        {
            _directoryEntry.CommitChanges();
        }
        catch (Exception e)
        {
            throw e;
        }

        return this;
    }

    public DirectoryEntry Build()
    {
        return _directoryEntry;
    }


}