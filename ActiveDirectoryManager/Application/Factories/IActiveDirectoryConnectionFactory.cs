using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManager.Application;

internal interface IActiveDirectoryConnectionFactory
{
    public string Domain { get; }
    public PrincipalContext Connect();
}