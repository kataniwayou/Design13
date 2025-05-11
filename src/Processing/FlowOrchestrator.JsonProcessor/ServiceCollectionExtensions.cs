using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.JsonProcessor;

/// <summary>
/// Extension methods for registering JSON processor services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the JSON processor services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddJsonProcessor(this IServiceCollection services)
    {
        // Register the JSON processor factory
        services.AddSingleton<IJsonProcessorFactory, JsonProcessorFactory>();

        return services;
    }
}
