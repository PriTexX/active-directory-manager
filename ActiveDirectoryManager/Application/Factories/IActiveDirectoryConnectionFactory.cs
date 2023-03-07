using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManager.Application;

public interface IActiveDirectoryConnectionFactory
{
    public string Domain { get; }
    public PrincipalContext Connect();
    public Task<PrincipalContext> ConnectAsync();
}