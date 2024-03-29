﻿using System.Runtime.Versioning;

namespace ActiveDirectoryManager.Core;

[SupportedOSPlatform("windows")]
public sealed class GroupItem : DomainItem
{
    public string? Info
    {
        get => GetAttribute("info");
        set => SetAttribute("info", value);
    }
}