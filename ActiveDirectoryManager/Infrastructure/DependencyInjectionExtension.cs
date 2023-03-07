using System.Runtime.Versioning;
using ActiveDirectoryManager.Application;
using ActiveDirectoryManager.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

[SupportedOSPlatform("windows")]
public static class DependencyInjectionExtension
{
    public static IServiceCollection AddActiveDirectoryServices(this IServiceCollection services, 
        string domain, string user, string password, 
        Func<IDomainSearcherBuilder, IDomainSearcherBuilder> domainSearcherConfigurator)
    {
        var adFactory = ActiveDirectoryManagerFactory
            .Create()
            .SetConnectionOptions(domain, user, password)
            .ConfigureDomainSearcher(domainSearcherConfigurator)
            .Build();

        services.AddSingleton<IActiveDirectoryManager>(d => adFactory.GetActiveDirectoryManager());
        services.AddSingleton<IDomainSearcher>(d => adFactory.GetDomainSearcher());
        
        return services;
    }
}