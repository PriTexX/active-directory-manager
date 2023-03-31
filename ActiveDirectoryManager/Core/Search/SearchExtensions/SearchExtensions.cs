using ActiveDirectoryManager.Application;

namespace ActiveDirectoryManager.Core;

public static class SearchExtensions
{
    public static ItemSearcher Find(this IDomainSearcher searcher)
    {
        return new ItemSearcher(searcher);
    }
}