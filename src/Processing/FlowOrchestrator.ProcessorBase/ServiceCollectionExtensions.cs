using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Extension methods for registering processor base services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the processor base services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddProcessorBase(this IServiceCollection services)
    {
        // No services to register at this level
        return services;
    }
}
