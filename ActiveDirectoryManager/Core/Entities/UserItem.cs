﻿using System.Runtime.Versioning;

namespace ActiveDirectoryManager.Core;

[SupportedOSPlatform("windows")]
public sealed class UserItem : DomainItem
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

    public string? Company
    {
        get => GetAttribute("company");
        set => SetAttribute("company", value);
    }

    public void SetPassword(string newPassword)
    {
        GetUnderlyingObject().Invoke("SetPassword", newPassword);
        GetUnderlyingObject().Properties["LockOutTime"].Value = 0;
    }

    public async Task SetPasswordAsync(string newPassword)
    {
        await Task.Run(() => SetPassword(newPassword));
    }
}