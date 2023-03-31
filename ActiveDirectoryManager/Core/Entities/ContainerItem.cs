using System.Runtime.Versioning;

namespace ActiveDirectoryManager.Core;

[SupportedOSPlatform("windows")]
public sealed class ContainerItem : DomainItem
{
    public override void Remove()
    {
        GetUnderlyingObject().DeleteTree();
    }

    public override async Task RemoveAsync()
    {
        await Task.Run(Remove);
    }

}