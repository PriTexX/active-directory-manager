using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;
using ActiveDirectoryManager.Application;

namespace ActiveDirectoryManager.Infrastructure;

[SupportedOSPlatform("windows")]
public sealed class ActiveDirectoryConnectionFactory : IActiveDirectoryConnectionFactory
{
    private PrincipalContext? _context;
    private readonly string _domain;
    private readonly string _username;
    private readonly string _password;

    public ActiveDirectoryConnectionFactory(string domain, string username, string password)
    {
        _domain = domain;
        _username = username;
        _password = password;
        Domain = domain;
    }

    public string Domain { get; }

    public PrincipalContext Connect()
    {
        return _context ??= new PrincipalContext(ContextType.Domain, _domain, _username, _password);
    }

    public Task<PrincipalContext> ConnectAsync()
    {
        throw new NotImplementedException();
    }
}