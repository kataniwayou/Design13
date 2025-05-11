using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Extension methods for registering transformation engine services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the transformation engine services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddTransformationEngine(this IServiceCollection services)
    {
        // Register the transformation engine services
        services.AddSingleton<ITransformationRuleManager, TransformationRuleManager>();
        services.AddSingleton<ITransformationPerformanceOptimizer, TransformationPerformanceOptimizer>();
        services.AddSingleton<ITransformationErrorHandler, TransformationErrorHandler>();
        services.AddSingleton<ITransformationEngine, TransformationEngine>();
        
        return services;
    }
}
