using System.DirectoryServices.AccountManagement;
using ActiveDirectoryManager.Application.Factories;

namespace ActiveDirectoryManager.Infrastructure.Common;

public sealed class ActiveDirectoryConnectionFactory : IActiveDirectoryConnectionFactory
{
    private readonly string _domain;
    private readonly string _username;
    private readonly string _password;

    public ActiveDirectoryConnectionFactory(string domain, string username, string password)
    {
        _domain = domain;
        _username = username;
        _password = password;
    }

    public PrincipalContext Connect()
    {
        throw new NotImplementedException();
    }

    public Task<PrincipalContext> ConnectAsync()
    {
        throw new NotImplementedException();
    }
}