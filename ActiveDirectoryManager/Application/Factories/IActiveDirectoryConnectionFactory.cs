using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManager.Application;

public interface IActiveDirectoryConnectionFactory
{
    public PrincipalContext Connect();
    public Task<PrincipalContext> ConnectAsync();
}