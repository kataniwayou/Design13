using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Extension methods for registering validation processor services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the validation processor services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddValidationProcessor(this IServiceCollection services)
    {
        // Register the validator factory
        services.AddSingleton<IValidatorFactory, ValidatorFactory>();
        
        // Register the validation processor factory
        services.AddSingleton<IValidationProcessorFactory, ValidationProcessorFactory>();
        
        return services;
    }
}
