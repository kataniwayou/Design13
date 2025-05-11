using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.MappingProcessor;

/// <summary>
/// Extension methods for registering mapping processor services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the mapping processor services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddMappingProcessor(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddSingleton<IMapper>(provider =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Configure AutoMapper here
            });
            
            return config.CreateMapper();
        });
        
        // Register the mapping processor factory
        services.AddSingleton<IMappingProcessorFactory, MappingProcessorFactory>();
        
        return services;
    }
}
