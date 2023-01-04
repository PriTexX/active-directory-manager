using ActiveDirectoryManager.ConnectionFactory;

namespace ActiveDirectoryManager;

public class ActiveDirectoryManager
{
    private IActiveDirectoryConnectionFactory _connectionFactory;

    public ActiveDirectoryManager(IActiveDirectoryConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
}