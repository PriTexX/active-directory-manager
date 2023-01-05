using ActiveDirectoryManager.Core.Entities;
using ActiveDirectoryManager.Core.Search;

namespace ActiveDirectoryManager.Application.Factories;

public interface IFilterBuilder
{
    public string BuildSearchFilter(QueryFilter queryFilter, DomainItemType type);

    public (string, IEnumerator<QueryFilter>, bool) BuildSearchFilter(IEnumerator<QueryFilter> enumerator, int maxCount,
        DomainItemType type);

    public (string, IEnumerator<string>, bool) BuildSearchFilter(IEnumerator<string> enumerator, int maxCount,
        DomainItemType type, string propertyName);
}