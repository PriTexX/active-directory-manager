using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManager.ConnectionFactory;

public interface IActiveDirectoryConnectionFactory
{
    public PrincipalContext Connect();
}