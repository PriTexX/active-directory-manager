using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManager.Application.Factories;

public interface IActiveDirectoryConnectionFactory
{
    public PrincipalContext Connect();
    public Task<PrincipalContext> ConnectAsync();
}