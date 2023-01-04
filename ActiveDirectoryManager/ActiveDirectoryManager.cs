using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManager;

public class ActiveDirectoryManager
{
    private PrincipalContext _context;

    public PrincipalContext GetUnderlyingContext()
    {
        return _context;
    } 
}