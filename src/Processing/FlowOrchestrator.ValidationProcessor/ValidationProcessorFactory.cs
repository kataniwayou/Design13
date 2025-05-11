using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Factory for creating validation processors.
/// </summary>
public class ValidationProcessorFactory : IValidationProcessorFactory
{
    private readonly IValidatorFactory _validatorFactory;
    private readonly ILoggerFactory _loggerFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationProcessorFactory"/> class.
    /// </summary>
    /// <param name="validatorFactory">The validator factory.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public ValidationProcessorFactory(
        IValidatorFactory validatorFactory,
        ILoggerFactory loggerFactory)
    {
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
    
    /// <inheritdoc />
    public ValidationProcessor CreateProcessor(string processorId, string name, string description)
    {
        var logger = _loggerFactory.CreateLogger<ValidationProcessor>();
        return new ValidationProcessor(processorId, name, description, _validatorFactory, logger);
    }
}
