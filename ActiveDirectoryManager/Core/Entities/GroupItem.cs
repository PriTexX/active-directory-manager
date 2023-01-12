namespace ActiveDirectoryManager.Core.Entities;

public sealed class GroupItem : DomainItem
{
    public string? Info
    {
        get => GetAttribute("info");
        set => SetAttribute("info", value);
    }
}