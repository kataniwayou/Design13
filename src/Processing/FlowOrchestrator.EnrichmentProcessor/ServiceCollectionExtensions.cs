using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Extension methods for registering enrichment processor services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the enrichment processor services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddEnrichmentProcessor(this IServiceCollection services)
    {
        // Register the enrichment source provider
        services.AddSingleton<IEnrichmentSourceProvider, EnrichmentSourceProvider>();
        
        // Register the enrichment processor factory
        services.AddSingleton<IEnrichmentProcessorFactory, EnrichmentProcessorFactory>();
        
        return services;
    }
}
