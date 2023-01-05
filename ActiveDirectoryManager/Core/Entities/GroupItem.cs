namespace ActiveDirectoryManager.Core.Entities;

public class GroupItem : DomainItem
{
    public string? Info
    {
        get => GetAttribute("info");
        set => SetAttribute("info", value);
    }
}