using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Factory for creating validators.
/// </summary>
public class ValidatorFactory : IValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public ValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    /// <inheritdoc />
    public IValidator? GetValidator(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        
        // Try to get the validator from the service provider
        var validator = _serviceProvider.GetService(type) as IValidator;
        if (validator != null)
        {
            return validator;
        }
        
        // If no validator is registered, create a default validator
        if (type == typeof(IValidator<object>))
        {
            return new DefaultObjectValidator();
        }
        
        return null;
    }
    
    /// <inheritdoc />
    public IValidator<T>? GetValidator<T>()
    {
        // Try to get the validator from the service provider
        var validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator != null)
        {
            return validator;
        }
        
        // If no validator is registered and T is object, create a default validator
        if (typeof(T) == typeof(object))
        {
            return new DefaultObjectValidator() as IValidator<T>;
        }
        
        return null;
    }
}
