using ActiveDirectoryManager.Application.Common;
using ActiveDirectoryManager.Application.Factories;

namespace ActiveDirectoryManager.Infrastructure.Common;

public sealed class ActiveDirectoryManager : IActiveDirectoryManager
{
    private IActiveDirectoryConnectionFactory _connectionFactory;

    public ActiveDirectoryManager(IActiveDirectoryConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
}