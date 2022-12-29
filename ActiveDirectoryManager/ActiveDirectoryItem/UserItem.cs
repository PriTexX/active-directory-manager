namespace ActiveDirectoryManager.ActiveDirectoryItem;

public class UserItem : DomainItem
{
    public bool? Enabled
    {
        get
        {
            const int adsIfAccountDisable = 0x00000002;
            var userAccountControl = Convert.ToInt32(GetAttribute("useraccountcontrol"));
            return !Convert.ToBoolean(userAccountControl & adsIfAccountDisable);
        }
    }

    public string? FirstName
    {
        get => GetAttribute("givenname");
        set => SetAttribute("givenname", value);
    }

    public string? Surname
    {
        get => GetAttribute("sn");
        set => SetAttribute("sn", value);
    }

    public string? DisplayName
    {
        get => GetAttribute("displayname");
        set => SetAttribute("displayname", value);
    }

    public string? UserPrincipalName
    {
        get => GetAttribute("userprincipalname");
        set => SetAttribute("userprincipalname", value);
    }

    public string? SamAccountName
    {
        get => GetAttribute("samaccountname");
        set => SetAttribute("samaccountname", value);
    }

    public string? EmployeeNumber
    {
        get => GetAttribute("employeenumber");
        set => SetAttribute("employeenumber", value);
    }

    public string? Mail
    {
        get => GetAttribute("mail");
        set => SetAttribute("mail", value);
    }

    public string? Department
    {
        get => GetAttribute("department");
        set => SetAttribute("department", value);
    }

    public List<string?>? Company
    {
        get => GetListAttributes("company");
        set => SetListAttributes("company", value);
    }
}