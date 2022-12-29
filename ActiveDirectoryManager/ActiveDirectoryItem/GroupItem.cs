namespace ActiveDirectoryManager.ActiveDirectoryItem;

public class GroupItem : DomainItem
{
    public string? Info
    {
        get => GetAttribute("info");
        set => SetAttribute("info", value);
    }
}