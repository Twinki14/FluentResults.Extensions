using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentResults.Extensions.Microservice;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResultFactory(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton(typeof(IResultFactory<>), typeof(ResultFactory<>)));

        return services;
    }
}
